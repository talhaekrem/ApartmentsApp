import * as Yup from 'yup';
import { useState, useEffect } from 'react';
import { useFormik, Form, FormikProvider } from 'formik';
import { useNavigate } from 'react-router-dom';
// material
import { Stack, TextField, Switch, Autocomplete, FormControlLabel } from '@mui/material';
import { LoadingButton } from '@mui/lab';
import axios from 'axios';

export default function AddHouse() {
    const [users, setUsers] = useState([]);
    useEffect(() => {
        axios("https://localhost:44309/api/Users")
            .then((res) => setUsers(res.data))
            .catch((e) => console.log(e))
    }, []);
    if (users.entityList == null) {
        users.entityList = []
    };

    const navigate = useNavigate();

    const schema = Yup.object().shape({
        Id: Yup.number(),
        OwnerId: Yup.number(),
        IsOwned: Yup.bool(),
        BlockName: Yup.string().max(25, 'Blok adı çok uzun').required('Blok Adı zorunludur'),
        HomeType: Yup.string().min(1, 'Ev tipi çok kısa').max(50, 'Ev tipi çok uzun').required('Ev tipi zorunludur'),
        FloorNumber: Yup.number().min(0, 'Ev, 0 dan küçük katta olamaz').required('Kat bilgisi gereklidir'),
        DoorNumber: Yup.number().min(0, 'Kapı numarası 0 dan küçük olamaz').required('Kapı numarası zorunludur')
    });


    const formik = useFormik({
        initialValues: {
            Id: 0,
            OwnerId: 0,
            IsOwned: false,
            BlockName: "",
            HomeType: "",
            FloorNumber: "",
            DoorNumber: ""
        },
        validationSchema: schema,
        onSubmit: (newHouse) => {
            fetch("/api/Houses", {
                method: "POST",
                body: JSON.stringify(newHouse),
                headers: { 'Content-Type': 'application/json' },
            }).then(response => response.json())
                .then(navigate('/admin/houses', { replace: true }));
            //axios.post("/api/Houses", { newHouse })
            //    .then(resp => console.log(resp))
            //    .then(navigate('/admin/houses', { replace: true }))
            //    .catch(e => console.log(e));

        }
    });
    const { errors, touched, handleSubmit, isSubmitting, getFieldProps, values } = formik;


    return (
        <FormikProvider value={formik}>
            <Form onSubmit={handleSubmit}>
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

                    <LoadingButton
                        fullWidth
                        size="large"
                        type="submit"
                        variant="contained"
                        loading={isSubmitting}
                    >
                        Ekle
                    </LoadingButton>
                </Stack>
            </Form>
        </FormikProvider>
    );
}