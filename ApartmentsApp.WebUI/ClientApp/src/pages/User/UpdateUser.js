import * as Yup from 'yup';
import { useState, useEffect } from 'react';
import {useParams} from 'react-router-dom';

import { useFormik, Form, FormikProvider } from 'formik';
// material
import { Stack, TextField, Switch, FormControlLabel, Alert,Button, Typography, Container } from '@mui/material';
import axios from 'axios';
import Page from '../../components/Page';

export default function UpdateUser() {
    //gelen evin idsi
    const {userId} = useParams();

    //idye göre evi getiriyorum
    const [currentUser, setCurrentUser] = useState({});
    
    useEffect(() => {
        axios.get(`/api/Users/${userId}`)
        .then(res => setCurrentUser(res.data.entity))
        .catch(err => console.error(err));
    }, [userId]);
    //validasyon şeması
    const schema = Yup.object().shape({
        Id: Yup.number(),
        TcNo: Yup.string().min(11, "Tc Kimlik No kısa").max(11, "Tc kimlik No uzun").required("Tc Kimlik No gereklidir"),
        Name: Yup.string().max(50, 'İsim çok uzun').required('İsim gereklidir'),
        SurName: Yup.string().max(50, 'Soyisim çok uzun').required('Soyisim gereklidir'),
        DisplayName: Yup.string().max(100, 'Görünen isim çok uzun').required('Görünen isim gereklidir'),
        Email: Yup.string().email('Geçerli bir e posta adresi giriniz').required('Email gereklidir'),
        PhoneNumber: Yup.string().min(0, 'Telefon numarası çok kısa').max(50, "Telefon numarası çok uzun").required('Telefon numarası gereklidir'),
        CarPlate: Yup.string().min(0, 'Plaka çok kısa').max(50, "Plaka çok uzun"),
        IsAdmin: Yup.bool(),
        IsDeleted: Yup.bool()
    });
    const [result, setResult] = useState([]);
    const formik = useFormik({
        enableReinitialize: true,
        initialValues: {
            Id:currentUser.id,
            TcNo:currentUser.tcNo ?? "",
            Name: currentUser.name ?? "",
            SurName: currentUser.surName ?? "",
            DisplayName:currentUser.displayName ?? "",
            Email: currentUser.email ?? "",
            PhoneNumber: currentUser.phoneNumber ?? "",
            CarPlate:currentUser.CarPlate ?? "",
            IsAdmin: currentUser.isAdmin ?? false,
            IsDeleted: currentUser.isDeleted ?? false
        },
        validationSchema: schema,
        onSubmit: (newUser) => {
            fetch("/api/Users", {
                method: "PUT",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newUser)})
                .then(resp=> resp.json())
                .then(data => setResult(data))
        }
    });
    const { errors, touched, handleSubmit, getFieldProps, values } = formik;

    return (

        <Page title="Kullanıcıyı Güncelle | My Apartments">
            <Container>
            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Kullanıcıyı Güncelle
                    </Typography>
                </Stack>
            <FormikProvider value={formik}>

            <Form onSubmit={handleSubmit}>
                <Stack spacing={2} mb={3}>
                    { result.isSuccess === true &&
                <Alert severity="success">Kullanıcı başarıyla güncellendi</Alert>
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
                            label="Gözüken Ad"
                            {...getFieldProps('DisplayName')}
                            error={Boolean(touched.DisplayName && errors.DisplayName)}
                            helperText={touched.DisplayName && errors.DisplayName}
                        />

                        <TextField
                            fullWidth
                            label="TC Kimlik No"
                            {...getFieldProps('TcNo')}
                            error={Boolean(touched.TcNo && errors.TcNo)}
                            helperText={touched.TcNo && errors.TcNo}
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
                            fullWidth
                            label="Araç Plakası (yoksa boş bırakın)"
                            {...getFieldProps('CarPlate')}
                            error={Boolean(touched.CarPlate && errors.CarPlate)}
                            helperText={touched.CarPlate && errors.CarPlate}
                        />

                        <FormControlLabel
                            control={<Switch {...getFieldProps('IsAdmin')} checked={values.IsAdmin} />}
                            label="Bu kişi yönetici mi?"
                        />
                    </Stack>

                    <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                    <FormControlLabel
                            control={<Switch {...getFieldProps('IsDeleted')} checked={values.IsDeleted} />}
                            label="Hesap devre dışı mı?"
                        />
                    </Stack>

                    <Button
                    color="warning"
                        fullWidth
                        size="large"
                        type="submit"
                        variant="contained"
                    >
                        Güncelle
                    </Button>
                </Stack>
            </Form>
        </FormikProvider>
            </Container>
        </Page>
    )
}
