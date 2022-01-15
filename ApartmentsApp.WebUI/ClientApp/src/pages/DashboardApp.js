// material
import { Box, Grid, Container, Typography, Button } from '@mui/material';
// components
import Page from '../components/Page';
import { Link as RouterLink } from 'react-router-dom';
// ----------------------------------------------------------------------

export default function DashboardApp() {
    return (
        <Page title="Admin | My Apartments">
            <Container maxWidth="xl">
                <Box sx={{ pb: 5 }}>
                    <Typography variant="h4">Selam, Tekrardan Hoş Geldin</Typography>
                </Box>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={6} md={3}>
                        <Button
                            fullWidth
                            color="primary"
                            variant="contained"
                            component={RouterLink}
                            to={`/admin/houses`}
                        >
                            Evler
                        </Button>
                    </Grid>
                    <Grid item xs={12} sm={6} md={3}>
                        <Button
                            fullWidth
                            color="secondary"
                            variant="contained"
                            component={RouterLink}
                            to={`/admin/users`}
                        >
                            Kullanıcılar
                        </Button>
                    </Grid>
                    <Grid item xs={12} sm={6} md={3}>
                        <Button
                            fullWidth
                            color="info"
                            variant="contained"
                            component={RouterLink}
                            to={`/admin/bills`}
                        >
                            Faturalar
                        </Button>
                    </Grid>
                    <Grid item xs={12} sm={6} md={3}>
                        <Button
                            fullWidth
                            color="success"
                            variant="contained"
                            component={RouterLink}
                            to={`/admin/messages`}
                        >
                            Mesajlar
                        </Button>
                    </Grid>
                </Grid>
            </Container>
        </Page>
    );
}
