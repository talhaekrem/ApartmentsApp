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
import { BillAdminListHead, BillAdminListToolbar } from '../../components/_dashboard/billsAdmin';

//request
import axios from "axios";
// ----------------------------------------------------------------------
const cursorPointer = {cursor:"pointer"};
const TABLE_HEAD = [
    { id: 'homeId', label: 'Evin Idsi', alignRight: false },
    { id: 'isHomeBillPaid', label: 'Aidat', alignRight: false },
    { id: 'isElectricBillPaid', label: 'Elektrik', alignRight: false },
    { id: 'isWaterBillPaid', label: 'Su', alignRight: false },
    { id: 'isGasBillPaid', label: 'Doğalgaz', alignRight: false },
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
    if (query) {
        return filter(array, (row) => row.homeId.toString().toLowerCase().indexOf(query.toLowerCase()) !== -1);
    }
    return stabilizedThis.map((el) => el[0]);
}

export default function BillAdmin() {
    //loading kısmı
    const [loading, setLoading] = useState(true);
    const [billAdmin, setBillAdmin] = useState({});
    useEffect(() => {
        axios("/api/BillAdmin")
            .then((res) => setBillAdmin(res.data))
            .catch((e) => console.log(e))
            .finally(() => setLoading(false));
    }, []);
    if (billAdmin.entityList == null) {
        billAdmin.entityList = []
    }
    const [page, setPage] = useState(0);
    const [order, setOrder] = useState('asc');
    const [orderBy, setOrderBy] = useState('homeId');
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

    const emptyRows = page > 0 ? Math.max(0, (1 + page) * rowsPerPage - billAdmin.entityList.length) : 0;

    const filteredUsers = applySortFilter(billAdmin.entityList, getComparator(order, orderBy), filterName);

    const isUserNotFound = filteredUsers.length === 0;

    return (
        <Page title="Fatura ve Aidatlar | My Apartments">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Fatura ve Aidat
                    </Typography>
                    <Button
                        variant="contained"
                        component={RouterLink}
                        to="add"
                        startIcon={<Icon icon={plusFill} />}
                    >
                        Toplu Fatura Ekle
                    </Button>
                    <Button
                        variant="contained"
                        component={RouterLink}
                        to="add"
                        startIcon={<Icon icon={plusFill} />}
                    >
                        Yeni Fatura Ekle
                    </Button>
                </Stack>

                <Card>
                    <BillAdminListToolbar
                        filterName={filterName}
                        onFilterName={handleFilterByName}
                    />

                    <Scrollbar>
                        <TableContainer sx={{ minWidth: 800 }}>
                            <Table>
                                <BillAdminListHead
                                    order={order}
                                    orderBy={orderBy}
                                    headLabel={TABLE_HEAD}
                                    rowCount={billAdmin.entityList.length}
                                    onRequestSort={handleRequestSort}
                                />
                                <TableBody>
                                    {filteredUsers
                                        .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                        .map((row, index) => {
                                            const { id,
                                                homeId,
                                                isHomeBillPaid,
                                                homeBillActive,
                                                isElectricBillPaid,
                                                electricBillActive,
                                                isWaterBillPaid,
                                                waterBillActive,
                                                isGasBillPaid,
                                                gasBillActive } = row;

                                            return (
                                                <TableRow
                                                    hover
                                                    key={index}
                                                    tabIndex={-1}
                                                >
                                                    <TableCell align="left">{homeId}</TableCell>
                                                    <TableCell align="left">
                                                        {
                                                            homeBillActive === true ? (
                                                                <Button 
                                                                color="secondary"
                                                                    size="small"
                                                                    to={`dues/${id}`}
                                                                    component={RouterLink}>
                                                                <Label
                                                                style={cursorPointer}
                                                                    variant="ghost"
                                                                    color={(isHomeBillPaid === false && 'error') || 'info'}>
                                                                    {isHomeBillPaid ? "Ödendi" : "Ödenmedi"}
                                                                </Label>
                                                                </Button>
  
                                                            ) : (
                                                                <Button
                                                                color="secondary"
                                                                    size="small"
                                                                    to={`dues/${id}`}
                                                                    component={RouterLink}>
                                                                <Label
                                                                style={cursorPointer}
                                                                    variant="ghost"
                                                                    color="secondary">
                                                                    Fatura Kesilmedi
                                                                </Label>
                                                                </Button>

                                                            )
                                                        }
                                                    </TableCell>
                                                    <TableCell align="left">
                                                        {
                                                            electricBillActive === true ? (
                                                                <Button
                                                                color="secondary"
                                                                    size="small"
                                                                    component={RouterLink}
                                                                    to={`electric/${id}`}>
                                                                <Label
                                                                style={cursorPointer}
                                                                    variant="ghost"
                                                                    color={(isElectricBillPaid === false && 'error') || 'info'}>
                                                                    {isElectricBillPaid ? "Ödendi" : "Ödenmedi"}
                                                                    </Label>
                                                                </Button>

                                                            ) : (
                                                                <Button
                                                                color="secondary"
                                                                    size="small"
                                                                    component={RouterLink}
                                                                    to={`electric/${id}`}
                                                                >
                                                                    <Label
                                                                    style={cursorPointer}
                                                                    variant="ghost"
                                                                    color="secondary">
                                                                    Fatura Kesilmedi
                                                                </Label>
                                                                </Button>
                                                                
                                                            )
                                                        }
                                                    </TableCell>

                                                    <TableCell align="left">
                                                        {
                                                            waterBillActive === true ? (
                                                                <Button
                                                                color="secondary"
                                                                    size="small"
                                                                    component={RouterLink}
                                                                    to={`water/${id}`}
                                                                >
                                                                    <Label
                                                                    style={cursorPointer}
                                                                    variant="ghost"
                                                                    color={(isWaterBillPaid === false && 'error') || 'info'}>
                                                                    {isWaterBillPaid ? "Ödendi" : "Ödenmedi"}
                                                                </Label>
                                                                </Button>
                                                                
                                                            ) : (
                                                                <Button
                                                                color="secondary"
                                                                    size="small"
                                                                    component={RouterLink}
                                                                    to={`water/${id}`}
                                                                >
                                                                    <Label
                                                                    style={cursorPointer}
                                                                    variant="ghost"
                                                                    color="secondary">
                                                                    Fatura Kesilmedi
                                                                </Label>
                                                                </Button>
                                                                
                                                            )
                                                        }
                                                    </TableCell>

                                                    <TableCell align="left">
                                                        {
                                                            gasBillActive === true ? (
                                                                <Button
                                                                color="secondary"
                                                                    size="small"
                                                                    component={RouterLink}
                                                                    to={`gas/${id}`}
                                                                >
                                                                    <Label
                                                                    style={cursorPointer}
                                                                    variant="ghost"
                                                                    color={(isGasBillPaid === false && 'error') || 'info'}>
                                                                    {isGasBillPaid ? "Ödendi" : "Ödenmedi"}
                                                                </Label>
                                                                </Button>
                                                                
                                                            ) : (
                                                                <Button
                                                                color="secondary"
                                                                    size="small"
                                                                    component={RouterLink}
                                                                    to={`gas/${id}`}
                                                                >
                                                                    <Label
                                                                    style={cursorPointer}
                                                                    variant="ghost"
                                                                    color="secondary">
                                                                    Fatura Kesilmedi
                                                                </Label>
                                                                </Button>
                                                                
                                                            )
                                                        }
                                                    </TableCell>
                                                </TableRow>
                                            );
                                        })}
                                    {emptyRows > 0 && (
                                        <TableRow style={{ height: 53 * emptyRows }}>
                                            <TableCell colSpan={5} />
                                        </TableRow>
                                    )}
                                </TableBody>
                                {loading && (
                                    <TableBody>
                                        <TableRow>
                                            <TableCell align="center" colSpan={5} sx={{ py: 3 }}>
                                                <CircularProgress />
                                            </TableCell>
                                        </TableRow>
                                    </TableBody>
                                )}
                                {isUserNotFound && (
                                    <TableBody>
                                        <TableRow>
                                            <TableCell align="center" colSpan={5} sx={{ py: 3 }}>
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
                        count={billAdmin.entityList.length}
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
