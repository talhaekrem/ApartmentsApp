import * as Yup from 'yup';
import { useState } from 'react';
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
} from '@mui/material';
import { LoadingButton } from '@mui/lab';

// ----------------------------------------------------------------------

export default function LoginForm() {
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);

  const LoginSchema = Yup.object().shape({
    tcNo: Yup.string().min(11,"TC kimlik numarası 11 haneden oluşmalıdır").max(11,"TC kimlik numarası 11 haneden oluşmalıdır").required('TC kimlik no gereklidir'),
    password: Yup.string().required('Şifre gereklidir')
  });

  const formik = useFormik({
    initialValues: {
      tcNo: '',
      password: '',
    },
    validationSchema: LoginSchema,
    onSubmit: (login) => {
      fetch("/Account/Login",{
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        credentials :"include",
        body: JSON.stringify(login)
      })
    navigate('/dashboard', { replace: true });
    }
  });

  const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

  const handleShowPassword = () => {
    setShowPassword((show) => !show);
  };
  return (
    <FormikProvider value={formik}>
      <Form onSubmit={handleSubmit}>
        <Stack spacing={3} sx={{mb:4}}>
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

        <LoadingButton
          fullWidth
          size="large"
          type="submit"
          variant="contained"
          loading={isSubmitting}
        >
          Login
        </LoadingButton>
      </Form>
    </FormikProvider>
  );
}
