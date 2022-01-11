import { filter } from 'lodash';
import { Icon } from '@iconify/react';
import { useState, useEffect } from 'react';
import plusFill from '@iconify/icons-eva/plus-fill';
import { Link as RouterLink } from 'react-router-dom';
// material
import {
    Card,
    Table,
    Stack,
    Button,
    TableRow,
    TableBody,
    TableCell,
    Container,
    Typography,
    TableContainer,
    TablePagination,
    CircularProgress
} from '@mui/material';
// components
import Page from '../../components/Page';
import Label from '../../components/Label';
import Scrollbar from '../../components/Scrollbar';
import SearchNotFound from '../../components/SearchNotFound';
import { UserListHead, UserListToolbar, UserMoreMenu } from '../../components/_dashboard/users';

//request
import axios from "axios";
// ----------------------------------------------------------------------

const TABLE_HEAD = [
    { id: 'tcNo', label: 'Tc Kimlik No', alignRight: false },
    { id: 'name', label: 'Ad', alignRight: false },
    { id: 'surName', label: 'Soyad', alignRight: false },
    { id: 'email', label: 'E Posta', alignRight: false },
    { id: 'phoneNumber', label: 'Telefon No', alignRight: false },
    { id: 'blockName', label: 'Kaldığı Blok Adı', alignRight: false },
    { id: 'doorNumber', label: 'Kapı No', alignRight: false },
    { id: 'isAdmin', label: 'Rol', alignRight: false },
    { id: '' }
];

// ----------------------------------------------------------------------

function descendingComparator(a, b, orderBy) {
    if (b[orderBy] < a[orderBy]) {
        return -1;
    }
    if (b[orderBy] > a[orderBy]) {
        return 1;
    }
    return 0;
}
function getComparator(order, orderBy) {
    return order === 'desc'
        ? (a, b) => descendingComparator(a, b, orderBy)
        : (a, b) => -descendingComparator(a, b, orderBy);
}

function applySortFilter(array, comparator, query) {
    const stabilizedThis = array.map((el, index) => [el, index]);
    stabilizedThis.sort((a, b) => {
        const order = comparator(a[0], b[0]);
        if (order !== 0) return order;
        return a[1] - b[1];
    });
    //(_user) => _user.name.toLowerCase().indexOf(query.toLowerCase()) !== -1
    if (query) {
        return filter(array, function(row){
            if(row.name.toLowerCase().indexOf(query.toLowerCase()) === -1){
                return row.surName.toLowerCase().indexOf(query.toLowerCase()) !== -1;
            }else{
                return row.name.toLowerCase().indexOf(query.toLowerCase()) !== -1;
            }
        });
    }
    return stabilizedThis.map((el) => el[0]);
}

export default function User() {
    //loading kısmı
    const [loading,setLoading] = useState(true);
    const [user, setUser] = useState({});
    useEffect(() => {
        axios("/api/Users")
            .then((res) => setUser(res.data))
            .catch((e) => console.log(e))
            .finally(() => setLoading(false));
    }, []);
    if (user.entityList == null) {
        user.entityList = []
    }
    const [page, setPage] = useState(0);
    const [order, setOrder] = useState('asc');
    const [orderBy, setOrderBy] = useState('name');
    const [filterName, setFilterName] = useState('');
    const [rowsPerPage, setRowsPerPage] = useState(5);

    const handleRequestSort = (event, property) => {
        const isAsc = orderBy === property && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
    };

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleFilterByName = (event) => {
        setFilterName(event.target.value);
    };

    const emptyRows = page > 0 ? Math.max(0, (1 + page) * rowsPerPage - user.entityList.length) : 0;

    const filteredUsers = applySortFilter(user.entityList, getComparator(order, orderBy), filterName);

    const isUserNotFound = filteredUsers.length === 0;

    return (
        <Page title="Kullanıcılar | My Apartments">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Kullanıcılar
                    </Typography>
                    <Button
                        variant="contained"
                        component={RouterLink}
                        to="add"
                        startIcon={<Icon icon={plusFill} />}
                    >
                        Yeni Kullanıcı Ekle
                    </Button>
                </Stack>

                <Card>
                    <UserListToolbar
                        filterName={filterName}
                        onFilterName={handleFilterByName}
                    />

                    <Scrollbar>
                        <TableContainer sx={{ minWidth: 800 }}>
                            <Table>
                                <UserListHead
                                    order={order}
                                    orderBy={orderBy}
                                    headLabel={TABLE_HEAD}
                                    rowCount={user.entityList.length}
                                    onRequestSort={handleRequestSort}
                                />
                                <TableBody>
                                    {filteredUsers
                                        .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                        .map((row, index) => {
                                            const { id, tcNo, name, surName, email, phoneNumber, blockName, doorNumber, isAdmin } = row;
  
                                            return (
                                                <TableRow
                                                    hover
                                                    key={index}
                                                    tabIndex={-1}
                                                >
                                                    <TableCell align="left">{tcNo}</TableCell>
                                                    <TableCell align="left">{name}</TableCell>
                                                    <TableCell align="left">{surName}</TableCell>
                                                    <TableCell align="left">{email}</TableCell>
                                                    <TableCell align="left">{phoneNumber}</TableCell>
                                                    <TableCell align="left">{blockName}</TableCell>
                                                    <TableCell align="left">{doorNumber ?? "Yok"}</TableCell>
                                                    <TableCell align="left">
                                                        <Label
                                                            variant="ghost"
                                                            color={(isAdmin === false && 'primary') || 'info'}>
                                                            {isAdmin ? "Admin" : "User"}
                                                        </Label>
                                                    </TableCell>
                                                    <TableCell align="right">
                                                        <UserMoreMenu userId={id}/>
                                                    </TableCell>
                                                </TableRow>
                                            );
                                        })}
                                    {emptyRows > 0 && (
                                        <TableRow style={{ height: 53 * emptyRows }}>
                                            <TableCell colSpan={8} />
                                        </TableRow>
                                    )}
                                </TableBody>
                                {loading && (
                                    <TableBody>
                                        <TableRow>
                                            <TableCell align="center" colSpan={8} sx={{py:3}}>
                                            <CircularProgress />
                                            </TableCell>
                                        </TableRow>
                                    </TableBody>
                                )}
                                {isUserNotFound && (
                                    <TableBody>
                                        <TableRow>
                                            <TableCell align="center" colSpan={8} sx={{ py: 3 }}>
                                                <SearchNotFound searchQuery={filterName} />
                                            </TableCell>
                                        </TableRow>
                                    </TableBody>
                                )}
                            </Table>
                        </TableContainer>
                    </Scrollbar>

                    <TablePagination
                        rowsPerPageOptions={[5, 10, 25]}
                        component="div"
                        count={user.entityList.length}
                        rowsPerPage={rowsPerPage}
                        page={page}
                        onPageChange={handleChangePage}
                        onRowsPerPageChange={handleChangeRowsPerPage}
                    />
                </Card>
            </Container>
        </Page>
    );
}
