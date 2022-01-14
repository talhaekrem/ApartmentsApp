import * as Yup from 'yup';
import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { useFormik, Form, FormikProvider } from 'formik';
// material
import {
    Stack,
    Container,
    Typography,
    Alert,
    TextField,
    Button
} from '@mui/material';
import Page from '../../components/Page';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import DateTimePicker from '@mui/lab/DesktopDatePicker';
export default function DetailBillAdmin() {
    const [result, setResult] = useState([]);
    const [bill, setBill] = useState({});
    const [billName, setBillName] = useState("");
    const { billId } = useParams();
    const { type } = useParams();
    const DeleteClick = () => {
        axios.delete(`/api/BillAdmin/${type}/${billId}`)
            .then(res => setResult(res.data));
    }

    useEffect(() => {
        axios.get(`/api/BillAdmin/${type}/${billId}`)
            .then((res) => setBill(res.data.entity));

        switch (type) {
            case "dues":
                setBillName("Aidat");
                break;
            case "electric":
                setBillName("Elektrik");
                break;
            case "water":
                setBillName("Su");
                break;
            case "gas":
                setBillName("Doğalgaz");
                break;
            default:
                break;
        }
    }, [type, billId]);

    //validasyon şeması
    const schema = Yup.object().shape({
        Id: Yup.number(),
        HomeId: Yup.number(),
        BillsId: Yup.number(),
        IsPaid: Yup.bool(),
        Price: Yup.number().required("Fiyat zorunludur"),
        BillDate: Yup.date().required("Fatura kesim tarihi zorunludur")
    });

    const formik = useFormik({
        enableReinitialize: true,
        initialValues: {
            Id: bill?.id ?? 0,
            HomeId: bill?.homeId,
            BillsId: bill?.billsId,
            IsPaid: bill?.isPaid ?? false,
            Price: bill?.price ?? 0,
            BillDate: bill?.billDate ?? new Date().now
        },
        validationSchema: schema,
        onSubmit: (updateBill) => {
            if (bill == null) {
                fetch(`/api/BillAdmin/AddOne/${type}/${billId}`, {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(updateBill)
                })
                    .then(resp => resp.json())
                    .then(data => setResult(data))
            }
            else {
                fetch(`/api/BillAdmin/${type}`, {
                    method: "PUT",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(updateBill)
                })
                    .then(resp => resp.json())
                    .then(data => setResult(data))
            }

        }
    });
    const { errors, touched, setFieldValue, handleSubmit, getFieldProps, values } = formik;
    return (
        <Page title="Fatura-Aidat | My Apartments">
            <Container>
                <Stack direction="column" alignItems="flex-start" justifyContent="flex-start" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Güncelle &amp; Detay
                    </Typography>
                    <p>Faturayı güncellemek isterseniz, yeni verileri girip güncelle butonuna tıklamanız yeterlidir.</p>
                    <p> Faturayı silmek isterseniz Sil butonuna tıklamanız yeterli olacaktır. </p>
                </Stack>

                <Typography variant="h4" gutterBottom>
                    {billName}
                </Typography>

                <Typography variant="h6" gutterBottom>
                    <p>Fatura Kesim Tarihi: {bill?.billDate ?? "Veri Yok"}</p>
                    <p>Ödeme Tarihi: {bill?.paymentDate ?? "Veri Yok"}</p>
                </Typography>

                <Stack spacing={2} mb={3}>
                    {result.isSuccess === true &&
                        <Alert severity="success">Fatura başarıyla güncellendi</Alert>
                    }
                    {result.isSuccess === false && <Alert severity='error'>{result.exeptionMessage}</Alert>
                    }

                </Stack>
                <FormikProvider value={formik}>
                    <Form onSubmit={handleSubmit}>

                        <Stack direction="column" spacing={2}>
                            <TextField
                                fullWidth
                                label="Fiyat"
                                {...getFieldProps('Price')}
                                error={Boolean(touched.Price && errors.Price)}
                                helperText={touched.Price && errors.Price}
                            />
                            <LocalizationProvider dateAdapter={AdapterDateFns}>
                                <DateTimePicker
                                    renderInput={(params) => <TextField {...params} />}
                                    label="Fatura Kesim Tarihi"
                                    value={values?.BillDate}
                                    inputFormat="dd/MM/yyyy"
                                    onChange={(data) => { setFieldValue("BillDate", data) }}
                                />
                            </LocalizationProvider>


                            <Stack direction="row" spacing={5} my={5}>
                                    <Button
                                    onClick={DeleteClick}
                                    disabled={bill === null ? true : false}
                                        color="error"
                                        fullWidth
                                        size="large"
                                        variant="contained"
                                    >
                                        Sil
                                    </Button>

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


                        </Stack>
                    </Form>
                </FormikProvider>

            </Container>
        </Page>
    )
}
