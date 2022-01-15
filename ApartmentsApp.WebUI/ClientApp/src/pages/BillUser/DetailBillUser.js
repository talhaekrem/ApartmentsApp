import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
import axios from 'axios';
// material
import {
    Stack,
    Container,
    Typography,
    Alert,
    Card,
    Table,
    TableContainer,
    TableBody,
    TableRow,
    TableCell,
    Button,
    FormLabel,
    FormControl,
    FormControlLabel,
    RadioGroup,
    Radio
} from '@mui/material';
import Page from '../../components/Page';
import Label from '../../components/Label';
import Scrollbar from '../../components/Scrollbar';
import { Formik, Form } from 'formik';

export default function DetailBillAdmin() {
    const [result, setResult] = useState([]);
    const [bill, setBill] = useState({});
    const [billName, setBillName] = useState("");
    const { billId } = useParams();
    const { type } = useParams();

    const [card, setCard] = useState([]);

    useEffect(() => {
        axios.get(`/api/BillUser/detail/${type}/${billId}`)
            .then((res) => setBill(res.data.entity));

        axios.get("/Account/ProfileDetails")
            .then((res) => {
                axios.get(`/api/CreditCard/GetMyCards/${res.data.entity.id}`)
                    .then((resp) => setCard(resp.data))
            })

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
                setBillName("");
                break;
        }
    }, [type, billId]);


    return (
        <Page title="Fatura-Aidat Öde | My Apartments">
            <Container>
                <Stack direction="column" alignItems="flex-start" justifyContent="flex-start" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        {billName} Detay
                    </Typography>
                </Stack>

                <Stack spacing={2} mb={3}>
                    {result === true &&
                        <Alert severity="success">Fatura başarıyla ödendi</Alert>
                    }
                    {result === false && <Alert severity='error'>Fatura ödenirken bir hata oluştu</Alert>}
                </Stack>

                <Card>
                    <Scrollbar>
                        <TableContainer>
                            <Table>
                                <TableBody>
                                    <TableRow>
                                        <TableCell align="left">Fatura Id</TableCell>
                                        <TableCell align="left">{bill?.id}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Ödendi mi?</TableCell>
                                        <TableCell align="left">
                                            {bill && (
                                                <Label
                                                    variant="ghost"
                                                    color={(bill.isPaid === false && 'error') || 'success'}>
                                                    {(bill.isPaid ? "Evet" : "Hayır")}
                                                </Label>
                                            )}

                                        </TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Tutar</TableCell>
                                        <TableCell align="left">{bill && bill.price + " TL"} </TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Eklenme Tarihi</TableCell>
                                        <TableCell align="left">{bill?.billDate}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Ödenme Tarihi</TableCell>
                                        <TableCell align="left">{bill?.paymentDate === null ? "Henüz Ödemediniz" : bill?.paymentDate}</TableCell>
                                    </TableRow>
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </Scrollbar>
                </Card>

                {bill && (!bill.isPaid && (
                    <Stack sx={{ my: 5 }}>

                        <Card sx={{ mb: 3 }}>
                            <Typography variant="h6" gutterBottom>
                                Yalnızca Kredi Kartıyla Ödeme yapabilirsiniz
                            </Typography>
                        </Card>

                        {card !== null ? (

                            <Formik
                                enableReinitialize="true"
                                initialValues={{
                                    cardId: "",
                                    total:bill.price ?? 0
                                }}
                                onSubmit={(datas) => {
                                    fetch("/api/CreditCard/Pay", {
                                        method: "POST",
                                        headers: { 'Content-Type': 'application/json' },
                                        body: JSON.stringify(datas)
                                    })
                                        .then(resp => resp.json())
                                        .then(data => {
                                            if (data === true) {
                                                axios.get(`/api/BillUser/pay/${type}/${billId}`)
                                                    .then((res) => setResult(res.data));
                                            }
                                        })
                                }}
                            >
                                {({ values, setFieldValue }) => (
                                    <Form>
                                        <FormControl component="fieldset">
                                            <FormLabel component="legend">Kart Seçiniz</FormLabel>
                                            <RadioGroup
                                                name="cardId"
                                                value={values.cardId}
                                                row
                                                aria-label="cards"
                                                onChange={(event) => {
                                                    setFieldValue("cardId", event.currentTarget.value)
                                                }}
                                            >
                                                {card.map((row, index) => {
                                                    const { id, bankName } = row;
                                                    return (
                                                        <FormControlLabel key={index} value={id} control={<Radio />} label={bankName} />
                                                    );
                                                })}
                                            </RadioGroup>
                                        </FormControl>
                                        <Button
                                            type="submit"
                                            color="info"
                                            fullWidth
                                            size="large"
                                            variant="contained"
                                        >
                                            Öde
                                        </Button>
                                    </Form>
                                )}
                            </Formik>

                        ) : (
                            <p>Hesabınıza kayıtlı kredi kartı bulunamadı. Profilimden hesabınıza kredi kartı ekleyiniz.</p>
                        )}
                    </Stack>
                ))}
            </Container>
        </Page>
    )
}