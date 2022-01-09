import {useState, useEffect} from 'react'
import {useParams} from 'react-router-dom';
import axios from 'axios';
// material
import {
    Card,
    Table,
    Stack,
    Container,
    TableContainer,
    TableBody,
    TableRow,
    TableCell,
    Typography
} from '@mui/material';
import Page from '../../components/Page';
import Label from '../../components/Label';
import Scrollbar from '../../components/Scrollbar';
export default function DetailHouse() {
    const [house, setHouse] = useState({});
    const {houseId} = useParams();
    useEffect(() => {
        axios.get(`/api/Houses/${houseId}`)
        .then((res) => setHouse(res.data.entity))
    }, [houseId]);
    return (
        <Page title="Ev Detayı | My Apartments">
            <Container>
            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Ev Detayı
                    </Typography>
                </Stack>
                <Card>
                    <Scrollbar>
                        <TableContainer>
                            <Table>
                                <TableBody>

                                    <TableRow>
                                        <TableCell align="left">Evin idsi</TableCell>
                                        <TableCell align="left">{house.id}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Ev aktif mi</TableCell>
                                        <TableCell align="left">
                                        <Label
                                            variant="ghost"
                                            color={(house.isActive === false && 'error') || 'success'}>
                                                {(house.isActive ? "Aktif" : "Pasif")}
                                                </Label>
                                        </TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Evin durumu</TableCell>
                                        <TableCell align="left">
                                            <Label
                                            variant="ghost"
                                            color={(house.isOwned === false && 'error') || 'success'}>
                                                {(house.isOwned ? "Ev Dolu" : "Ev Boş")}
                                                </Label>
                                        </TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Ev sahibinin idsi</TableCell>
                                        <TableCell align="left">{house.ownerId ?? "Yok"}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Blok Adı</TableCell>
                                        <TableCell align="left">{house.blockName}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Evin Tipi</TableCell>
                                        <TableCell align="left">{house.homeType}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Bulunduğu kat</TableCell>
                                        <TableCell align="left">{house.floorNumber}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Kapı numarası</TableCell>
                                        <TableCell align="left">{house.doorNumber}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Eklenme tarihi</TableCell>
                                        <TableCell align="left">{house.insertDate}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Güncellenme tarihi</TableCell>
                                        <TableCell align="left">{house.updateDate === null ? "Yok" : house.updateDate}</TableCell>
                                    </TableRow>
                                </TableBody>
                            </Table>
                        </TableContainer>

                    </Scrollbar>
                </Card>
            </Container>
        </Page>
    )
}
