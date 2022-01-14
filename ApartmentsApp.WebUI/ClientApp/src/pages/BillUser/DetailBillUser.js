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
    Button
} from '@mui/material';
import Page from '../../components/Page';
import Label from '../../components/Label';
import Scrollbar from '../../components/Scrollbar';

export default function DetailBillAdmin() {
    const [result, setResult] = useState([]);
    const [bill, setBill] = useState({});
    const [billName, setBillName] = useState("");
    const { billId } = useParams();
    const { type } = useParams();

    const payClick = () => {
        console.log("öde tıklandı");
    };
    useEffect(() => {
        axios.get(`/api/BillUser/detail/${type}/${billId}`)
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
                    {result.isSuccess === true &&
                        <Alert severity="success">Fatura başarıyla ödendi</Alert>
                    }
                    {result.isSuccess === false && <Alert severity='error'>{result.exeptionMessage}</Alert>}
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
                                        <TableCell align="left">{bill && bill.price + "TL"} </TableCell>
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
                            kart bilgileri gelecek
                        </Card>

                        <Button
                            onClick={payClick}
                            color="info"
                            fullWidth
                            size="large"
                            variant="contained"
                        >
                            Öde
                        </Button>
                    </Stack>
                ))}
            </Container>
        </Page>
    )
}