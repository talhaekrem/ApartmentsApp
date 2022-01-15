// material
import { Box, Grid, Container, Typography, Button } from '@mui/material';
// components
import Page from '../components/Page';
import { Link as RouterLink } from 'react-router-dom';

// ----------------------------------------------------------------------

export default function UserDashboard() {
    return (
        <Page title="Home | My Apartments">
            <Container maxWidth="xl">
                <Box sx={{ pb: 5 }}>
                    <Typography variant="h4">Selam, Tekrardan Hoş Geldin</Typography>
                </Box>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={6}>
                        <Button
                            fullWidth
                            color="success"
                            variant="contained"
                            component={RouterLink}
                            to={`/dashboard/bills`}
                        >
                            Faturalar
                        </Button>
                    </Grid>
                    <Grid item xs={12} sm={6} >
                        <Button
                            fullWidth
                            color="info"
                            variant="contained"
                            component={RouterLink}
                            to={`/dashboard/messages`}
                        >
                            Mesajlar
                        </Button>
                    </Grid>
                </Grid>
            </Container>
        </Page>
    );
}
