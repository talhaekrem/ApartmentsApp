import { Icon } from '@iconify/react';
import { useRef, useState } from 'react';
import editFill from '@iconify/icons-eva/edit-fill';
import { Link as RouterLink } from 'react-router-dom';
import trash2Outline from '@iconify/icons-eva/trash-2-outline';
import moreVerticalFill from '@iconify/icons-eva/more-vertical-fill';
import arrowIosForwardFill from '@iconify/icons-eva/arrow-ios-forward-fill';
import axios from 'axios';
// material
import { Menu,
   MenuItem,
  IconButton, 
  ListItemIcon, 
  ListItemText,
  Dialog, 
  DialogTitle, 
  DialogContent,
  DialogContentText,
  Button,
  Typography,
  Box,
  Modal,
  DialogActions 
} from '@mui/material';

// ----------------------------------------------------------------------

export default function UserMoreMenu(props) {
  const ref = useRef(null);
  const [isOpen, setIsOpen] = useState(false);
//alert ekranı için
const [dialog, setDialog] = useState(false);
const handleClose = () => {
    setDialog(false);
    setIsSuccess(false);
};

const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 400,
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

const openDialog = () => {
  //sil dendiği an alert(dialog) ekranını aç ve sil butonunun olduğu moreMenuyu kapa
  setDialog(true);
  setIsOpen(false);
};

const [isSuccess, setIsSuccess] = useState(false);
const [result, setResult] = useState({});
const DeleteSelected = () => {
  axios.delete(`/api/Users/${props.houseId}`)
  .then(res => setResult(res.data));
  setDialog(false);
  setIsSuccess(true);
}


  return (
    <>
        <Dialog
                open={dialog}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description">
                    <DialogTitle id="alert-dialog-title">
          {"Sil"}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
          Seçili kullanıcıyı devre dışı bırakmak istediğinizden emin misiniz?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Hayır</Button>
          <Button onClick={DeleteSelected} autoFocus>
            Evet
          </Button>
        </DialogActions>
        </Dialog>

        <Modal
        open={isSuccess}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            {result.isSuccess ? "Başarılı" : "Hata"}
          </Typography>
          <Typography id="modal-modal-description" sx={{ mt: 2 }}>
            {result.isSuccess ? "Kullanıcı başarıyla devre dışı bırakıldı" : result.exeptionMessage}
          </Typography>
        </Box>
      </Modal>
      <IconButton ref={ref} onClick={() => setIsOpen(true)}>
        <Icon icon={moreVerticalFill} width={20} height={20} />
      </IconButton>

      <Menu
        open={isOpen}
        anchorEl={ref.current}
        onClose={() => setIsOpen(false)}
        PaperProps={{
          sx: { width: 200, maxWidth: '100%' }
        }}
        anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
      >
                <MenuItem component={RouterLink} to={`detail/${props.userId}`} sx={{ color: 'text.secondary' }}>
                    <ListItemIcon>
                        <Icon icon={arrowIosForwardFill} width={24} height={24} />
                    </ListItemIcon>
                    <ListItemText primary="Detay" primaryTypographyProps={{ variant: 'body2' }} />
                </MenuItem>

                <MenuItem component={RouterLink} to={`update/${props.userId}`} sx={{ color: 'text.secondary' }}>
                    <ListItemIcon>
                        <Icon icon={editFill} width={24} height={24} />
                    </ListItemIcon>
                    <ListItemText primary="Güncelle" primaryTypographyProps={{ variant: 'body2' }} />
                </MenuItem>

                <MenuItem onClick={openDialog} sx={{ color: 'text.secondary' }}>
                    <ListItemIcon>
                        <Icon icon={trash2Outline} width={24} height={24} />
                    </ListItemIcon>
                    <ListItemText primary="Sil" primaryTypographyProps={{ variant: 'body2' }} />
                </MenuItem>
      </Menu>
    </>
  );
}
