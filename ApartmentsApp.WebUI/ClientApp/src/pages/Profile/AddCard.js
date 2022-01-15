import * as Yup from 'yup';
import { useState, useEffect } from 'react';
import { useFormik, Form, FormikProvider } from 'formik';
// material
import {
    Stack,
    TextField,
    Button,
    Typography,
    Container,
} from '@mui/material';
import axios from 'axios';
import Page from '../../components/Page';

export default function AddCard() {
    const [profile, setProfile] = useState({});
    useEffect(() => {
        axios.get("/Account/ProfileDetails")
            .then((res) => setProfile(res.data.entity))
    }, []);
    const schema = Yup.object().shape({
        BankName: Yup.string().required("Banka ismi zorunludur"),
        CardNo: Yup.string().min(16, "Kart numarası 16 basamalı olmakıdır").max(16, "Kart numarası 16 basamalı olmakıdır").required("Kart No zorunludur"),
        Month: Yup.number().min(1, "Ay 1 den küçük olamaz").max(12, "Ay 12 den büyük olamaz").required("Ay bilgisi zorunludur"),
        Year: Yup.number().min(new Date().getFullYear(), "Kartın yıl bilgisi mevcut yıldan küçük olamaz").required('Yıl bilgisi zorunludur'),
        CVC: Yup.string().min(3, 'Güvenlik şifresi 3 basamaklı olmalıdır').max(3, "güvenlik şifresi 3 basamaklı olmalıdır").required('Güvenlik şifresi gereklidir'),
        Balance: Yup.number().min(0, 'Bakiye 0 dan küçük olamaz').required('Bakiye zorunludur')
    });

    const formik = useFormik({
        enableReinitialize: true,
        initialValues: {
            UserId: profile.id ?? 0,
            BankName: "",
            CardNo: "",
            Month: 1,
            Year: new Date().getFullYear,
            CVC: "",
            Balance: 0
        },
        validationSchema: schema,
        onSubmit: (newCard) => {
            fetch("/api/CreditCard", {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newCard)
            });
        }
    });
    const { errors, touched, handleSubmit, getFieldProps } = formik;


    return (
        <Page title="Yeni Kart Ekle | My Apartments">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Yeni Kart Ekle
                    </Typography>
                </Stack>
                <FormikProvider value={formik}>

                    <Form onSubmit={handleSubmit}>
                        <Stack spacing={3}>
                            <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                                <TextField
                                    fullWidth
                                    label="Banka Adı"
                                    {...getFieldProps('BankName')}
                                    error={Boolean(touched.BankName && errors.BankName)}
                                    helperText={touched.BankName && errors.BankName}
                                />

                                <TextField
                                    fullWidth
                                    label="Kart Numarası"
                                    {...getFieldProps('CardNo')}
                                    error={Boolean(touched.CardNo && errors.CardNo)}
                                    helperText={touched.CardNo && errors.CardNo}
                                />
                            </Stack>

                            <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                                <TextField
                                    fullWidth
                                    label="Ay"
                                    {...getFieldProps('Month')}
                                    error={Boolean(touched.Month && errors.Month)}
                                    helperText={touched.Month && errors.Month}
                                />

                                <TextField
                                    fullWidth
                                    label="Yıl"
                                    {...getFieldProps('Year')}
                                    error={Boolean(touched.Year && errors.Year)}
                                    helperText={touched.Year && errors.Year}
                                />
                            </Stack>

                            <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                                <TextField
                                    fullWidth
                                    label="CVC"
                                    {...getFieldProps('CVC')}
                                    error={Boolean(touched.CVC && errors.CVC)}
                                    helperText={touched.CVC && errors.CVC}
                                />

                                <TextField
                                    fullWidth
                                    label="Bakiye"
                                    {...getFieldProps('Balance')}
                                    error={Boolean(touched.Balance && errors.Balance)}
                                    helperText={touched.Balance && errors.Balance}
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