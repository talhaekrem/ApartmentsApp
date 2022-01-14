import * as Yup from 'yup';
import { useState, useEffect } from 'react';
import { useFormik, Form, FormikProvider } from 'formik';
// material
import {
    Stack,
    TextField,
    Switch,
    Autocomplete,
    FormControlLabel,
    Alert,
    Button,
    Typography,
    Container,
    InputAdornment,
    Box
} from '@mui/material';
import Page from '../../components/Page';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import searchFill from '@iconify/icons-eva/search-fill';
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import DateTimePicker from '@mui/lab/DesktopDatePicker';
import axios from 'axios';
//icons
import { Icon } from '@iconify/react';
import baselineWaterDrop from '@iconify/icons-ic/baseline-water-drop';
import roundFlashOn from '@iconify/icons-ic/round-flash-on';
import bxLeaf from '@iconify/icons-bx/bx-leaf';
//icons
export default function AddUser() {
    const [result, setResult] = useState([]);
    const [houses, setHouses] = useState([]);

    useEffect(() => {
        axios("/api/Houses/GetBillableHomes")
            .then((res) => setHouses(res.data))
    }, []);
    if (houses.entityList == null) {
        houses.entityList = []
    };
    const schema = Yup.object().shape({
        HomeId: Yup.number().required(),
        ElectricPrice: Yup.number().required("Fiyat giriniz"),
        ElectricBillDate: Yup.date().default(() => { return new Date()}),
        WaterPrice: Yup.number().required("Fiyat giriniz"),
        WaterBillDate: Yup.date().default(() => { return new Date() }),
        GasPrice: Yup.number().required("Fiyat giriniz"),
        GasBillDate: Yup.date().default(() => { return new Date() }),
    });


    const formik = useFormik({
        initialValues: {
            HomeId: 0,
            Dues: false,
            Electric:false,
            ElectricPrice: 0,
            ElectricBillDate: new Date().now,
            Water: false,
            WaterPrice: 0,
            WaterBillDate: new Date().now,
            Gas: false,
            GasPrice: 0,
            GasBillDate: new Date().now,
            IsEveryone:false
        },
        validationSchema: schema,
        onSubmit: (newBill, { resetForm }) => {
            console.log(newBill);
            fetch("/api/BillAdmin", {
               method: "POST",
               headers: { 'Content-Type': 'application/json' },
               body: JSON.stringify(newBill)
            })
               .then(resp => resp.json())
               .then(data => setResult(data))
               .then(resetForm(newBill = ''))
        }
    });
    const { errors, touched,setFieldValue, handleSubmit, getFieldProps, values } = formik;


    return (
        <Page title="Yeni Fatura - Aidat Ekle | My Apartments">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Yeni Fatura veya Aidat Ekle
                    </Typography>
                </Stack>
                <FormikProvider value={formik}>

                    <Form onSubmit={handleSubmit}>
                        <Stack spacing={2} mb={3}>
                            {result.isSuccess === true &&
                                <Alert severity="success">Fatura başarıyla eklendi</Alert>
                            }
                            {result.isSuccess === false && <Alert severity='error'>{result.exeptionMessage}</Alert>}
                        </Stack>
                        <small>Aidat anahtarını aktifleştirdiğiniz takdirde mevcut kişinin aidat tutarı, bugünün tarihiyle otomatik olarak kendisine yansıtılacaktır.</small>
                        <small>Aidat tutarını görmek için Evler kısmından ilgili kişinin ev detayına göz atın.</small>

                        <Stack mb={5}>
                            <FormControlLabel
                                control={<Switch {...getFieldProps('Dues')} checked={(values.Dues)} />}
                                label="Aidat da ekleyeceğim"
                            />
                            <FormControlLabel
                                control={<Switch {...getFieldProps('IsEveryone')} checked={(values.IsEveryone)} />}
                                label="Tüm Herkese Ekle"
                            />
                            <Autocomplete
                                name="HomeId"
                                disabled={values.IsEveryone}
                                onChange={(event, data) => { values.HomeId = data?.id }}
                                sx={{ width: 300 }}
                                popupIcon={null}
                                getOptionLabel={(houses) => houses.ownerName}
                                options={houses.entityList}
                                renderOption={(props, option) => {
                                    return (
                                        <li {...props} key={option.id}>
                                            {option.ownerName}
                                        </li>
                                    );
                                }}
                                renderInput={(params) => (
                                    <TextField
                                        {...params}
                                        placeholder="Fatura sahibi..."
                                        InputProps={{
                                            ...params.InputProps,
                                            startAdornment: (
                                                <>
                                                    <InputAdornment position="start">
                                                        <Box
                                                            component={Icon}
                                                            icon={searchFill}
                                                            sx={{
                                                                ml: 1,
                                                                width: 20,
                                                                height: 20,
                                                                color: 'text.disabled'
                                                            }}
                                                        />
                                                    </InputAdornment>
                                                    {params.InputProps.startAdornment}
                                                </>
                                            )
                                        }}
                                    />
                                )}
                            />
                        </Stack>
                            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                                <Stack direction="column" alignItems="center" justifyContent="flex-start" spacing={1}>
                                <Icon icon={roundFlashOn} width={24} height={24} />
                                    <FormControlLabel
                                        control={<Switch {...getFieldProps('Electric')} checked={(values.Electric)} />}
                                        label="Elektrik"
                                    />
                                    <TextField
                                        fullWidth
                                        label="Fiyat"
                                        disabled={!values.Electric}
                                        {...getFieldProps('ElectricPrice')}
                                        error={Boolean(touched.ElectricPrice && errors.ElectricPrice)}
                                        helperText={touched.ElectricPrice && errors.ElectricPrice}
                                    />
                                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                                        <DateTimePicker
                                            disabled={!values.Electric}
                                            renderInput={(params) => <TextField {...params} />}
                                            label="Fatura Kesim Tarihi"
                                            value={values?.ElectricBillDate}
                                            inputFormat="dd/MM/yyyy"
                                            onChange={(data) => { setFieldValue("ElectricBillDate",data)}}
                                        />
                                    </LocalizationProvider>
                                </Stack>

                            <Stack direction="column" alignItems="center" justifyContent="flex-start" spacing={1}>
                            <Icon icon={baselineWaterDrop} width={24} height={24} />
                                <FormControlLabel
                                    control={<Switch {...getFieldProps('Water')} checked={(values.Water)} />}
                                    label="Su"
                                />
                                <TextField
                                    fullWidth
                                    label="Fiyat"
                                    disabled={!values.Water}
                                    {...getFieldProps('WaterPrice')}
                                    error={Boolean(touched.WaterPrice && errors.WaterPrice)}
                                    helperText={touched.WaterPrice && errors.WaterPrice}
                                />
                                <LocalizationProvider dateAdapter={AdapterDateFns}>
                                    <DateTimePicker
                                        disabled={!values.Water}
                                        renderInput={(params) => <TextField {...params} />}
                                        label="Fatura Kesim Tarihi"
                                        value={values?.WaterBillDate}
                                        inputFormat="dd/MM/yyyy"
                                        onChange={(data) => { setFieldValue("WaterBillDate",data)}}
                                    />
                                </LocalizationProvider>
                            </Stack>

                            <Stack direction="column" alignItems="center" justifyContent="flex-start" spacing={1}>
                            <Icon icon={bxLeaf} width={24} height={24}/>
                                <FormControlLabel
                                    control={<Switch {...getFieldProps('Gas')} checked={(values.Gas)} />}
                                    label="Doğalgaz"
                                />
                                <TextField
                                    fullWidth
                                    label="Fiyat"
                                    disabled={!values.Gas}
                                    {...getFieldProps('GasPrice')}
                                    error={Boolean(touched.GasPrice && errors.GasPrice)}
                                    helperText={touched.GasPrice && errors.GasPrice}
                                />
                                <LocalizationProvider dateAdapter={AdapterDateFns}>
                                    <DateTimePicker
                                        disabled={!values.Gas}
                                        renderInput={(params) => <TextField {...params} />}
                                        label="Fatura Kesim Tarihi"
                                        value={values?.GasBillDate}
                                        inputFormat="dd/MM/yyyy"
                                        onChange={(data) => { setFieldValue("GasBillDate",data)}}
                                    />
                                </LocalizationProvider>
                            </Stack>
                            </Stack>

                            <Button
                                fullWidth
                                size="large"
                                type="submit"
                                variant="contained"
                            >
                                Ekle
                            </Button>

                    </Form>
                </FormikProvider>
            </Container>
        </Page>

    );
}