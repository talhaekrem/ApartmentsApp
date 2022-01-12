import * as Yup from 'yup';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useFormik, Form, FormikProvider } from 'formik';
import { Icon } from '@iconify/react';
import eyeFill from '@iconify/icons-eva/eye-fill';
import eyeOffFill from '@iconify/icons-eva/eye-off-fill';
// material
import {
    Stack,
    TextField,
    IconButton,
    InputAdornment,
    Button,
    Alert
} from '@mui/material';

// ----------------------------------------------------------------------

export default function LoginForm() {
    const navigate = useNavigate();
    const [showPassword, setShowPassword] = useState(false);
    const [result, setResult] = useState([]);

    const LoginSchema = Yup.object().shape({
        tcNo: Yup.string().min(11, "TC kimlik numarası 11 haneden oluşmalıdır").max(11, "TC kimlik numarası 11 haneden oluşmalıdır").required('TC kimlik no gereklidir'),
        password: Yup.string().required('Şifre gereklidir')
    });

    useEffect(() => {
        fetch("/Account/Verify")
            .then(resp => resp.json())
            .then(data => {
                if (data.isSuccess) {
                    if (data.entity.isLogged) {
                        if (data.entity.isAdmin) {
                            navigate('/admin', { replace: true });
                        }
                        else {
                            navigate('/dashboard', { replace: true });
                        }
                    }
                }
            });
    }, [navigate]);

    const formik = useFormik({
        initialValues: {
            tcNo: '',
            password: '',
        },
        validationSchema: LoginSchema,
        onSubmit: (login) => {
            fetch("/Account/Login", {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(login)
            })
                .then(resp => resp.json())
                .then(data => {
                    if (data.isSuccess) {
                        if (data.entity.isLogged) {
                            if (data.entity.isAdmin) {
                                navigate('/admin', { replace: true });
                            }
                            else {
                                navigate('/dashboard', { replace: true });
                            }
                        }
                    }
                    else {
                        setResult(data);
                    }
                });
        }
    });

    const { errors, touched, handleSubmit, getFieldProps } = formik;

    const handleShowPassword = () => {
        setShowPassword((show) => !show);
    };

    return (
        <FormikProvider value={formik}>
            <Form onSubmit={handleSubmit}>
                <Stack spacing={2} mb={3}>
                    {result.isSuccess === false && <Alert severity='error'>{result.exeptionMessage}</Alert>

                    }
                </Stack>
                <Stack spacing={3} sx={{ mb: 4 }}>
                    <TextField
                        fullWidth
                        type="text"
                        label="TC Kimlik No"
                        {...getFieldProps('tcNo')}
                        error={Boolean(touched.tcNo && errors.tcNo)}
                        helperText={touched.tcNo && errors.tcNo}
                    />

                    <TextField
                        fullWidth
                        autoComplete="current-password"
                        type={showPassword ? 'text' : 'password'}
                        label="Şifre"
                        {...getFieldProps('password')}
                        InputProps={{
                            endAdornment: (
                                <InputAdornment position="end">
                                    <IconButton onClick={handleShowPassword} edge="end">
                                        <Icon icon={showPassword ? eyeFill : eyeOffFill} />
                                    </IconButton>
                                </InputAdornment>
                            )
                        }}
                        error={Boolean(touched.password && errors.password)}
                        helperText={touched.password && errors.password}
                    />
                </Stack>

                <Button
                    fullWidth
                    size="large"
                    type="submit"
                    variant="contained"
                >
                    Giriş Yap
                </Button>
            </Form>
        </FormikProvider>
    );
}
