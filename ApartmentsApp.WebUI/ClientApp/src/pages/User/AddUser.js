import * as Yup from 'yup';
import { useState } from 'react';
import { useFormik, Form, FormikProvider } from 'formik';
// material
import { Stack, TextField, Switch, FormControlLabel, Alert, Button, Typography, Container } from '@mui/material';
import Page from '../../components/Page';

export default function AddUser() {
    const [result, setResult] = useState([]);

    const schema = Yup.object().shape({
        TcNo: Yup.string().min(11, "Tc Kimlik No kısa").max(11, "Tc kimlik No uzun").required("Tc Kimlik No gereklidir"),
        Name: Yup.string().max(50, 'İsim çok uzun').required('İsim gereklidir'),
        SurName: Yup.string().max(50, 'Soyisim çok uzun').required('Soyisim gereklidir'),
        Email: Yup.string().email('Geçerli bir e posta adresi giriniz').required('Email gereklidir'),
        PhoneNumber: Yup.string().min(0, 'Telefon numarası çok kısa').max(50, "Telefon nymarası çok uzun").required('Telefon numarası gereklidir'),
        CarPlate: Yup.string().min(0, 'Plaka çok kısa').max(50, "Plaka çok uzun"),
        IsAdmin: Yup.bool()
    });


    const formik = useFormik({
        initialValues: {
            TcNo: "",
            Name: "",
            SurName: "",
            Email: "",
            PhoneNumber: "",
            CarPlate: "",
            IsAdmin: false
        },
        validationSchema: schema,
        onSubmit: (newUser, { resetForm }) => {
            fetch("/api/Users", {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newUser)
            })
                .then(resp => resp.json())
                .then(data => setResult(data))
                .then(resetForm(newUser = ''))
        }
    });
    const { errors, touched, handleSubmit, getFieldProps, values } = formik;


    return (
        <Page title="Yeni Kullanıcı Ekle | My Apartments">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Yeni Kullanıcı Ekle
                    </Typography>
                </Stack>
                <FormikProvider value={formik}>

                    <Form onSubmit={handleSubmit}>
                        <Stack spacing={2} mb={3}>
                            {result.isSuccess === true &&
                                <Alert severity="success">Kullanıcı başarıyla eklendi</Alert>
                            }
                            {result.isSuccess === false && <Alert severity='error'>{result.exeptionMessage}</Alert>

                            }

                        </Stack>
                        <Stack spacing={3}>
                            <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                                <TextField
                                    fullWidth
                                    label="Ad"
                                    {...getFieldProps('Name')}
                                    error={Boolean(touched.Name && errors.Name)}
                                    helperText={touched.Name && errors.Name}
                                />
                                <TextField
                                    fullWidth
                                    label="Soyad"
                                    {...getFieldProps('SurName')}
                                    error={Boolean(touched.SurName && errors.SurName)}
                                    helperText={touched.SurName && errors.SurName}
                                />
                            </Stack>

                            <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                                <TextField
                                    fullWidth
                                    label="Telefon No"
                                    {...getFieldProps('PhoneNumber')}
                                    error={Boolean(touched.PhoneNumber && errors.PhoneNumber)}
                                    helperText={touched.PhoneNumber && errors.PhoneNumber}
                                />

                                <TextField
                                    fullWidth
                                    label="E-Posta"
                                    {...getFieldProps('Email')}
                                    error={Boolean(touched.Email && errors.Email)}
                                    helperText={touched.Email && errors.Email}
                                />
                            </Stack>

                            <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                                <TextField
                                
                                    label="Tc Kimlik No"
                                    {...getFieldProps('TcNo')}
                                    error={Boolean(touched.TcNo && errors.TcNo)}
                                    helperText={touched.TcNo && errors.TcNo}
                                />

                                <FormControlLabel
                                    control={<Switch {...getFieldProps('IsAdmin')} checked={values.IsAdmin} />}
                                    label="Ekleyeceğiniz kişi Yönetici mi?"
                                />
                            </Stack>
                            <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                                <TextField
                                    fullWidth
                                    label="Araç Plakası (yoksa boş bırakın)"
                                    {...getFieldProps('CarPlate')}
                                    error={Boolean(touched.CarPlate && errors.CarPlate)}
                                    helperText={touched.CarPlate && errors.CarPlate}
                                />
                            </Stack>

                            <Button
                                fullWidth
                                size="large"
                                type="submit"
                                variant="contained"
                            >
                                Ekle
                            </Button>
                        </Stack>
                    </Form>
                </FormikProvider>
            </Container>
        </Page>

    );
}