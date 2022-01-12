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
export default function DetailUser() {
    const [user, setUser] = useState({});
    const {userId} = useParams();
    useEffect(() => {
        axios.get(`/api/Users/${userId}`)
        .then((res) => setUser(res.data.entity))
    }, [userId]);
    return (
        <Page title="Kullanıcı Detayı | My Apartments">
            <Container>
            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Kullanıcı Detayı
                    </Typography>
                </Stack>
                <Card>
                    <Scrollbar>
                        <TableContainer>
                            <Table>
                                <TableBody>

                                    <TableRow>
                                        <TableCell align="left">Kullanıcının idsi</TableCell>
                                        <TableCell align="left">{user.id}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Tc kimlik numarası</TableCell>
                                        <TableCell align="left">{user.tcNo}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Ad</TableCell>
                                        <TableCell align="left">{user.name}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Soyad</TableCell>
                                        <TableCell align="left">{user.surName}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">E posta</TableCell>
                                        <TableCell align="left">{user.email}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Telefon numarası</TableCell>
                                        <TableCell align="left">{user.phoneNumber}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Araç plakası</TableCell>
                                        <TableCell align="left">{user.carPlate ?? "Yok"}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Kullanıcını durumu</TableCell>
                                        <TableCell align="left">
                                            <Label
                                            variant="ghost"
                                            color={(user.isDeleted === true && 'error') || 'success'}>
                                                {(user.isDeleted ? "Devre dışı" : "Aktif")}
                                                </Label>
                                        </TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Kullanıcını rolü</TableCell>
                                        <TableCell align="left">
                                            <Label
                                            variant="ghost"
                                            color={(user.isAdmin === true && 'info') || 'primary'}>
                                                {(user.isAdmin ? "Admin" : "User")}
                                                </Label>
                                        </TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Eklenme tarihi</TableCell>
                                        <TableCell align="left">{user.insertDate}</TableCell>
                                    </TableRow>

                                    <TableRow>
                                        <TableCell align="left">Güncellenme tarihi</TableCell>
                                        <TableCell align="left">{user.updateDate === null ? "Yok" : user.updateDate}</TableCell>
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
