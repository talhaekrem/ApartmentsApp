import * as Yup from 'yup';
import { useState, useEffect } from 'react';
import { useFormik, Form, FormikProvider } from 'formik';
// material
import {
    Stack,
    TextField,
    Autocomplete,
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

export default function AddMessage() {
    const [result, setResult] = useState([]);
    const [users, setUsers] = useState([]);
    useEffect(() => {
        axios("/api/Messages/PopulateList")
            .then((res) => setUsers(res.data))
    }, []);
    if (users.entityList == null) {
        users.entityList = []
    };

    const schema = Yup.object().shape({
        ReceiverId: Yup.number(),
        MessageTitle: Yup.string().min(5, 'Başlık çok kısa').max(100, "Başlık çok uzun").required('Başlık zorunludur'),
        SenderMessage: Yup.string().min(5, 'Mesaj çok kısa').max(1000, "Mesaj çok uzun").required('Mesaj zorunludur')
    });

    const formik = useFormik({
        initialValues: {
            ReceiverId: 0,
            MessageTitle: "",
            SenderMessage: ""
        },
        validationSchema: schema,
        onSubmit: (newMessage, { resetForm }) => {
            fetch("/api/Messages/Send", {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newMessage)
            })
                .then(resp => resp.json())
                .then(data => setResult(data))
                .then(resetForm(newMessage = ''))
        }
    });
    const { errors, touched, handleSubmit, getFieldProps, values } = formik;


    return (
        <Page title="Yeni Mesaj Yaz | My Apartments">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        Yeni Mesaj Yaz
                    </Typography>
                </Stack>
                <FormikProvider value={formik}>

                    <Form onSubmit={handleSubmit}>
                        <Stack spacing={2} mb={3}>
                            {result.isSuccess === true &&
                                <Alert severity="success">Mesaj Başarıyla Yollandı</Alert>
                            }
                            {result.isSuccess === false && <Alert severity='error'>{result.exeptionMessage}</Alert>

                            }

                        </Stack>
                        <Stack spacing={3}>
                            <Autocomplete
                                name="ReceiverId"
                                onChange={(event, data) => { values.ReceiverId = data?.id }}
                                sx={{ width: 300 }}
                                popupIcon={null}
                                getOptionLabel={(user) => user.displayName}
                                options={users.entityList}
                                renderOption={(props, option) => {
                                    return (
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

                            <TextField
                                fullWidth
                                label="Başlık"
                                {...getFieldProps('MessageTitle')}
                                error={Boolean(touched.MessageTitle && errors.MessageTitle)}
                                helperText={touched.MessageTitle && errors.MessageTitle}
                            />

                            <TextField
                                fullWidth
                                label="İçerik"
                                {...getFieldProps('SenderMessage')}
                                error={Boolean(touched.SenderMessage && errors.SenderMessage)}
                                helperText={touched.SenderMessage && errors.SenderMessage}
                            />


                            <Button
                                fullWidth
                                size="large"
                                type="submit"
                                variant="contained"
                            >
                                Yolla
                            </Button>
                        </Stack>
                    </Form>
                </FormikProvider>
            </Container>
        </Page>

    );
}