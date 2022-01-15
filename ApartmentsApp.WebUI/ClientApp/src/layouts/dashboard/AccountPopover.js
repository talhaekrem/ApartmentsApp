import { Icon } from '@iconify/react';
import { useRef, useState, useEffect } from 'react';
import creditCardOutline from '@iconify/icons-eva/credit-card-outline';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
// material
import { alpha } from '@mui/material/styles';
import { Button, Box, Divider, MenuItem, Typography, Avatar, IconButton } from '@mui/material';
// components
import MenuPopover from '../../components/MenuPopover';
//
import axios from 'axios';
// ----------------------------------------------------------------------

export default function AccountPopover() {
    const anchorRef = useRef(null);
    const [open, setOpen] = useState(false);
    const navigate = useNavigate();
    const [profile, setProfile] = useState({});
    useEffect(() => {
        axios.get("/Account/ProfileDetails")
            .then((res) => setProfile(res.data.entity))
    }, []);
    const profileImage = "/static/mock-images/avatars/avatar_default.jpg";

    const handleOpen = () => {
        setOpen(true);
    };
    const handleClose = () => {
        setOpen(false);
    };
    const logout = () => {
        fetch("/account/logout", {
            method: "POST",
            headers: { "Content-Type": "application/json" }
        }).then(() => {
            navigate('/auth/login', { replace: true })
        }
        );
    }
    return (
        <>
            <IconButton
                ref={anchorRef}
                onClick={handleOpen}
                sx={{
                    padding: 0,
                    width: 44,
                    height: 44,
                    ...(open && {
                        '&:before': {
                            zIndex: 1,
                            content: "''",
                            width: '100%',
                            height: '100%',
                            borderRadius: '50%',
                            position: 'absolute',
                            bgcolor: (theme) => alpha(theme.palette.grey[900], 0.72)
                        }
                    })
                }}
            >
                <Avatar src={profileImage} alt="photoURL" />
            </IconButton>

            <MenuPopover
                open={open}
                onClose={handleClose}
                anchorEl={anchorRef.current}
                sx={{ width: 220 }}
            >
                <Box sx={{ my: 1.5, px: 2.5 }}>
                    <Typography variant="subtitle1" noWrap>
                        {profile.name + " " + profile.surName}
                    </Typography>
                    <Typography variant="body2" sx={{ color: 'text.secondary' }} noWrap>
                        {profile.email}
                    </Typography>
                </Box>

                <Divider sx={{ my: 1 }} />

                {profile.isAdmin === false && (
                    <MenuItem
                        to="/dashboard/addCard"
                        component={RouterLink}
                        onClick={handleClose}
                        sx={{ typography: 'body2', py: 1, px: 2.5 }}
                    >
                        <Box
                            component={Icon}
                            icon={creditCardOutline}
                            sx={{
                                mr: 2,
                                width: 24,
                                height: 24
                            }}
                        />
                        Kredi Kartı Ekle
                    </MenuItem>
                )}


                <Box sx={{ p: 2, pt: 1.5 }}>
                    <Button fullWidth color="inherit" variant="outlined" onClick={logout}>
                        Çıkış Yap
                    </Button>
                </Box>
            </MenuPopover>
        </>
    );
}
