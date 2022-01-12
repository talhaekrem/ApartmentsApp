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
import { Icon } from '@iconify/react';
import searchFill from '@iconify/icons-eva/search-fill';
import axios from 'axios';
import Page from '../../components/Page';

export default function AddHouse() {
    const [result, setResult] = useState([]);
    const [users, setUsers] = useState([]);
    useEffect(() => {
        axios("/api/Users/PopulateList")
            .then((res) => setUsers(res.data))
    }, []);
    if (users.entityList == null) {
        users.entityList = []
    };

    const schema = Yup.object().shape({
        OwnerId: Yup.number(),
        IsOwned: Yup.bool(),
        BlockName: Yup.string().max(25, 'Blok adı çok uzun').required('Blok Adı zorunludur'),
        HomeType: Yup.string().min(1, 'Ev tipi çok kısa').max(50, 'Ev tipi çok uzun').required('Ev tipi zorunludur'),
        FloorNumber: Yup.number().min(0, 'Ev, 0 dan küçük katta olamaz').required('Kat bilgisi gereklidir'),
        DoorNumber: Yup.number().min(0, 'Kapı numarası 0 dan küçük olamaz').required('Kapı numarası zorunludur')
    });

    const formik = useFormik({
        initialValues: {
            OwnerId: 0,
            IsOwned: false,
            BlockName: "",
            HomeType: "",
            FloorNumber: "",
            DoorNumber: ""
        },
        validationSchema: schema,
        onSubmit: (newHouse, { resetForm }) => {
            fetch("/api/Houses", {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newHouse)
            })
                .then(resp => resp.json())
                .then(data => setResult(data))
                .then(resetForm(newHouse = ''))
        }
    });
    const { errors, touched, handleSubmit, getFieldProps, values } = formik;


    return (
        <Page title="Yeni Ev Ekle | My Apartments">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Yeni Ev Ekle
                    </Typography>
                </Stack>
                <FormikProvider value={formik}>

                    <Form onSubmit={handleSubmit}>
                        <Stack spacing={2} mb={3}>
                            {result.isSuccess === true &&
                                <Alert severity="success">Ev ekleme işlemi başarılı</Alert>
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
                                        name="OwnerId"
                                        onChange={(event, data) => {values.OwnerId =data?.id}}
                                        sx={{width:300}}
                                        disabled={!values.IsOwned}
                                        popupIcon={null}
                                        getOptionLabel={(user) => user.displayName}
                                        options={users.entityList}
                                        renderOption={(props,option) =>{
                                            return(
                                                <li {...props} key={option.id}>
                                                    {option.displayName}
                                                </li>
                                            );
                                        }}
                                        renderInput={(params) => (
                                        <TextField
                                            {...params}
                                            placeholder="Kullanıcı seç..."
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

                            <Button
                                fullWidth
                                size="large"
                                type="submit"
                                variant="contained"
                            >
                                Ekle
                            </Button>
                        </Stack>
                    </Form>
                </FormikProvider>
            </Container>
        </Page>

    );
}