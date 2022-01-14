import { filter } from 'lodash';
import { Icon } from '@iconify/react';
import { useState, useEffect } from 'react';
import plusFill from '@iconify/icons-eva/plus-fill';
import arrowIosForwardFill from '@iconify/icons-eva/arrow-ios-forward-fill';
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
import { MessageListHead, MessageListToolbar } from '../../components/_dashboard/messages';

//request
import axios from "axios";
// ----------------------------------------------------------------------

const TABLE_HEAD = [
    { id: 'senderDisplayName', label: 'Mesajı Yollayan', alignRight: false },
    { id: 'IsSenderReaded', label: 'Yollayanın Durumu', alignRight: false },
    { id: 'receiverDisplayName', label: 'Mesajı Alan', alignRight: false },
    { id: 'IsReceiverReaded', label: 'Okuyanın Durumu', alignRight: false },
    { id: 'messageTitle', label: 'Konu Başlığı', alignRight: false },
    { id: 'insertDate', label: 'Oluşturma Tarihi', alignRight: false },
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
        return filter(array, function(row){
            if (row.messageTitle.toLowerCase().indexOf(query.toLowerCase()) === -1){
                return row.senderDisplayName.toLowerCase().indexOf(query.toLowerCase()) !== -1;
            }else{
                return row.messageTitle.toLowerCase().indexOf(query.toLowerCase()) !== -1;
            }
        });
    }
    return stabilizedThis.map((el) => el[0]);
}

export default function Message() {
    //loading kısmı
    const [loading,setLoading] = useState(true);
    const [message, setMessage] = useState({});
    useEffect(() => {
        axios("/api/Messages/GetMyMessages")
            .then((res) => setMessage(res.data))
            .catch((e) => console.log(e))
            .finally(() => setLoading(false));
    }, []);
    if (message.entityList == null) {
        message.entityList = []
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

    const emptyRows = page > 0 ? Math.max(0, (1 + page) * rowsPerPage - message.entityList.length) : 0;

    const filteredUsers = applySortFilter(message.entityList, getComparator(order, orderBy), filterName);

    const isUserNotFound = filteredUsers.length === 0;

    return (
        <Page title="Mesajlar | My Apartments">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Mesajlar
                    </Typography>
                    <Button
                        variant="contained"
                        component={RouterLink}
                        to="add"
                        startIcon={<Icon icon={plusFill} />}
                    >
                        Yeni Mesaj Yaz
                    </Button>
                </Stack>

                <Card>
                    <MessageListToolbar
                        filterName={filterName}
                        onFilterName={handleFilterByName}
                    />

                    <Scrollbar>
                        <TableContainer sx={{ minWidth: 800 }}>
                            <Table>
                                <MessageListHead
                                    order={order}
                                    orderBy={orderBy}
                                    headLabel={TABLE_HEAD}
                                    rowCount={message.entityList.length}
                                    onRequestSort={handleRequestSort}
                                />
                                <TableBody>
                                    {filteredUsers
                                        .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                        .map((row, index) => {
                                            const { id, senderDisplayName, isSenderReaded, receiverDisplayName, isReceiverReaded, messageTitle, insertDate } = row;
  
                                            return (
                                                <TableRow
                                                    hover
                                                    key={index}
                                                    tabIndex={-1}
                                                >
                                                    <TableCell align="left">{senderDisplayName}</TableCell>
                                                    <TableCell align="left">
                                                        <Label
                                                            variant="ghost"
                                                            color={(isSenderReaded === false && 'info') || 'success'}>
                                                            {isSenderReaded ? "Görüldü" : "İletildi"}
                                                        </Label>
                                                    </TableCell>
                                                    <TableCell align="left">{receiverDisplayName}</TableCell>
                                                    <TableCell align="left">
                                                        <Label
                                                            variant="ghost"
                                                            color={(isReceiverReaded === false && 'info') || 'success'}>
                                                            {isReceiverReaded ? "Görüldü" : "İletildi"}
                                                        </Label>
                                                    </TableCell>
                                                    <TableCell align="left">{messageTitle}</TableCell>
                                                    <TableCell align="left">{insertDate}</TableCell>
                                                    <TableCell align="right">
                                                    <Button
                                                    size="small"
                                                        variant="contained"
                                                        component={RouterLink}
                                                        to={`detail/${id}`}
                                                        endIcon={<Icon icon={arrowIosForwardFill} />}
                                                    >
                                                        Görüntüle
                                                    </Button>
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
                                {loading && (
                                    <TableBody>
                                        <TableRow>
                                            <TableCell align="center" colSpan={7} sx={{py:3}}>
                                            <CircularProgress />
                                            </TableCell>
                                        </TableRow>
                                    </TableBody>
                                )}
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
                        count={message.entityList.length}
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
