import { filter } from 'lodash';
import { Icon } from '@iconify/react';
import { sentenceCase } from 'change-case';
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
    TablePagination
} from '@mui/material';
// components
import Page from '../../components/Page';
import Label from '../../components/Label';
import Scrollbar from '../../components/Scrollbar';
import SearchNotFound from '../../components/SearchNotFound';
import { HouseListHead, HouseListToolbar, HouseMoreMenu } from '../../components/_dashboard/houses';
//
//import HOUSELIST from '../_mocks_/Houses';

//request
import axios from "axios";

// ----------------------------------------------------------------------

const TABLE_HEAD = [
    { id: 'Id', label: 'Id', alignRight: false },
    { id: 'OwnerDisplayName', label: 'Evin Sahipleri', alignRight: false },
    { id: 'IsActive', label: 'Evin Durumu', alignRight: false },
    { id: 'BlockName', label: 'Blok Adı', alignRight: false },
    { id: 'FloorNumber', label: 'Kat', alignRight: false },
    { id: 'DoorNumber', label: 'Kapı No', alignRight: false },
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
    if (query) {
        return filter(array, (_user) => _user.name.toLowerCase().indexOf(query.toLowerCase()) !== -1);
    }
    return stabilizedThis.map((el) => el[0]);
}

export default function House() {
    const [house, setHouse] = useState([]);
    useEffect(() => {
        axios("/api/Houses")
            .then((res) => setHouse(res.data))
            .catch((e) => console.log(e))
    }, []);
    if (house.entityList == null) {
        house.entityList = []
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

    const emptyRows = page > 0 ? Math.max(0, (1 + page) * rowsPerPage - house.entityList.length) : 0;

    const filteredUsers = applySortFilter(house.entityList, getComparator(order, orderBy), filterName);

    const isUserNotFound = filteredUsers.length === 0;

    return (
        <Page title="Evler | My Apartments">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        Evler
                    </Typography>
                    <Button
                        variant="contained"
                        component={RouterLink}
                        to="add"
                        startIcon={<Icon icon={plusFill} />}
                    >
                        Yeni Ev Ekle
                    </Button>
                </Stack>

                <Card>
                    <HouseListToolbar
                        filterName={filterName}
                        onFilterName={handleFilterByName}
                    />

                    <Scrollbar>
                        <TableContainer sx={{ minWidth: 800 }}>
                            <Table>
                                <HouseListHead
                                    order={order}
                                    orderBy={orderBy}
                                    headLabel={TABLE_HEAD}
                                    rowCount={house.entityList.length}
                                    onRequestSort={handleRequestSort}
                                />
                                <TableBody>
                                    {filteredUsers
                                        .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                        .map((row) => {
                                            const { Id, OwnerDisplayName, IsActive, BlockName, FloorNumber, DoorNumber } = row;

                                            return (
                                                <TableRow
                                                    hover
                                                    key={Id}
                                                    tabIndex={-1}
                                                >
                                                    <TableCell align="left">{Id}</TableCell>
                                                    <TableCell align="left" >{OwnerDisplayName}</TableCell>
                                                    <TableCell align="left">
                                                        <Label
                                                            variant="ghost"
                                                            color={(IsActive === true && 'error') || 'success'}
                                                        >
                                                            {sentenceCase(IsActive)}
                                                        </Label>
                                                    </TableCell>
                                                    <TableCell align="left">{BlockName}</TableCell>
                                                    <TableCell align="left">{FloorNumber}</TableCell>
                                                    <TableCell align="left">{DoorNumber}</TableCell>
                                                    <TableCell align="right">
                                                        <HouseMoreMenu />
                                                    </TableCell>
                                                </TableRow>
                                            );
                                        })}
                                    {emptyRows > 0 && (
                                        <TableRow style={{ height: 53 * emptyRows }}>
                                            <TableCell colSpan={7} />
                                        </TableRow>
                                    )}
                                </TableBody>
                                {isUserNotFound && (
                                    <TableBody>
                                        <TableRow>
                                            <TableCell align="center" colSpan={7} sx={{ py: 3 }}>
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
                        count={house.entityList.length}
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
