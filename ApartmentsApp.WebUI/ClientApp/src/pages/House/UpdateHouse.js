import * as Yup from 'yup';
import { useState, useEffect } from 'react';
import {useParams} from 'react-router-dom';

import { useFormik, Form, FormikProvider } from 'formik';
// material
import { Stack, TextField, Switch, Autocomplete, FormControlLabel, Alert,Button, Typography, Container } from '@mui/material';
import axios from 'axios';
import Page from '../../components/Page';

export default function UpdateHouse() {
    //gelen evin idsi
    const {houseId} = useParams();

    //idye göre evi getiriyorum
    const [currentHouse, setCurrentHouse] = useState({});
    const [users, setUsers] = useState([]);
    
    useEffect(() => {
        axios.get(`/api/Houses/${houseId}`)
        .then(res => setCurrentHouse(res.data.entity))
        .catch(err => console.error(err));

        axios("/api/Users")
        .then((res) => setUsers(res.data));
    }, [houseId]);
    if (users.entityList == null) {
        users.entityList = []
    };
    //validasyon şeması
    const schema = Yup.object().shape({
        Id: Yup.number(),
        OwnerId: Yup.number(),
        IsOwned: Yup.bool(),
        BlockName: Yup.string().max(25, 'Blok adı çok uzun').required('Blok Adı zorunludur'),
        HomeType: Yup.string().min(1, 'Ev tipi çok kısa').max(50, 'Ev tipi çok uzun').required('Ev tipi zorunludur'),
        FloorNumber: Yup.number().min(0, 'Ev, 0 dan küçük katta olamaz').required('Kat bilgisi gereklidir'),
        DoorNumber: Yup.number().min(0, 'Kapı numarası 0 dan küçük olamaz').required('Kapı numarası zorunludur')
    });
    const [result, setResult] = useState([]);
    const formik = useFormik({
        enableReinitialize: true,
        initialValues: {
            Id:currentHouse.id,
            OwnerId: currentHouse.ownerId ?? 0,
            IsOwned: currentHouse.isOwned ?? false,
            IsActive:currentHouse.isActive ?? false,
            BlockName: currentHouse.blockName ?? "",
            HomeType: currentHouse.homeType ?? "",
            FloorNumber: currentHouse.floorNumber ?? "",
            DoorNumber: currentHouse.doorNumber ?? ""
        },
        validationSchema: schema,
        onSubmit: (newHouse) => {
            fetch("/api/Houses", {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newHouse)})
                .then(resp=> resp.json())
                .then(data => setResult(data))
        }
    });
    const { errors, touched, handleSubmit, getFieldProps, values } = formik;

    return (

        <Page title="Evi Güncelle | My Apartments">
            <Container>
            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Evi Güncelle
                    </Typography>
                </Stack>
            <FormikProvider value={formik}>

            <Form onSubmit={handleSubmit}>
                <Stack spacing={2} mb={3}>
                    { result.isSuccess === true &&
                <Alert severity="success">Ev başarıyla güncellendi</Alert>
                }
                {result.isSuccess === false && <Alert severity='error'>{result.exeptionMessage}</Alert>
                }
                    
                </Stack>
                <Stack spacing={3}>
                    <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                        <TextField
                            fullWidth
                            label="Blok Adı"
                            {...getFieldProps('BlockName')}
                            error={Boolean(touched.BlockName && errors.BlockName)}
                            helperText={touched.BlockName && errors.BlockName}
                        />

                        <TextField
                            fullWidth
                            label="Ev Tipi"
                            {...getFieldProps('HomeType')}
                            error={Boolean(touched.HomeType && errors.HomeType)}
                            helperText={touched.HomeType && errors.HomeType}
                        />
                    </Stack>

                    <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                        <TextField
                            fullWidth
                            label="Kaçıncı Katta?"
                            {...getFieldProps('FloorNumber')}
                            error={Boolean(touched.FloorNumber && errors.FloorNumber)}
                            helperText={touched.FloorNumber && errors.FloorNumber}
                        />

                        <TextField
                            fullWidth
                            label="Kapı Numarası"
                            {...getFieldProps('DoorNumber')}
                            error={Boolean(touched.DoorNumber && errors.DoorNumber)}
                            helperText={touched.DoorNumber && errors.DoorNumber}
                        />
                    </Stack>

                    <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                        <FormControlLabel
                            control={<Switch {...getFieldProps('IsOwned')} checked={values.IsOwned} />}
                            label="Evin sahibi var mı?"
                        />
                        <Autocomplete
                            disabled={!values.IsOwned}
                            disablePortal
                            options={users.entityList}
                            sx={{ width: 300 }}
                            renderInput={(params) => <TextField {...params} label="Evin Sahibi" variant="standard" />}
                        />
                    </Stack>

                    <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                    <FormControlLabel
                            control={<Switch {...getFieldProps('IsActive')} checked={values.IsActive} />}
                            label="Ev Aktif mi?"
                        />
                    </Stack>

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
            </Form>
        </FormikProvider>
            </Container>
        </Page>
    )
}
