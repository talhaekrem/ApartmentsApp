// material
import { Box, Container, Typography } from '@mui/material';
// components
import Page from '../components/Page';

// ----------------------------------------------------------------------

export default function UserDashboard() {
  return (
    <Page title="Home | My Apartments">
      <Container maxWidth="xl">
        <Box sx={{ pb: 5 }}>
        <Typography variant="h2">User Sayfası</Typography>
          <Typography variant="h4">Selam, Tekrardan Hoş Geldin</Typography>
        </Box>

      </Container>
    </Page>
  );
}
