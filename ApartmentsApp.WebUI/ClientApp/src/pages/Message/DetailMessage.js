import * as Yup from 'yup';
import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import { useFormik, Form, FormikProvider } from 'formik';
// material
import {
    Stack,
    TextField,
    Alert, Button,
    Typography,
    Container,
    Card,
    CardContent
} from '@mui/material';
import axios from 'axios';
import Page from '../../components/Page';

export default function DetailMessage() {
    //gelen evin idsi
    const { messageId } = useParams();

    //idye göre evi getiriyorum
    const [currentMessage, setCurrentMessage] = useState({});
    const [isFinished, setIsFinished] = useState(false);
    const [isSender, setIsSender] = useState(false);
    useEffect(() => {
        axios.get(`/api/Messages/GetCurrentMessage/${messageId}`)
            .then(res => {
                setCurrentMessage(res.data.entity);
                //yollayan mesaj atmışsa konu kapanmıştır ve finished truedır
                //finished true ise form olmayacak
                if (res.data.entity.receiverMessage !== null) {
                    setIsFinished(true);
                }
            })
            .catch(err => console.error(err));
        axios.get(`/api/Messages/MessageStatus/${messageId}`)
            .then(res => {
                if (res.data.entity.isSender === true) {
                    setIsSender(true);
                }
                axios.put(`/api/Messages/SetReaded/${messageId}`);
                
            });
    }, [messageId]);
    //validasyon şeması
    const schema = Yup.object().shape({
        //Id: Yup.number(),
        //SenderId: Yup.number(),
        //ReceiverId: Yup.number(),
        //MessageTitle: Yup.string(),
        //IsSenderReaded: Yup.bool(),
        //IsReceiverReaded: Yup.bool(),
        //SenderMessage: Yup.string()
        ReceiverMessage: Yup.string().min(5, 'Mesaj çok kısa').max(1000, "Mesaj çok uzun").required('Mesaj zorunludur'),
    });
    const [result, setResult] = useState([]);
    const formik = useFormik({
        enableReinitialize: true,
        initialValues: {
            Id: currentMessage.id,
            SenderId: currentMessage.senderId ?? 0,
            ReceiverId: currentMessage.receiverId ?? 0,
            MessageTitle: currentMessage.messageTitle ?? "",
            IsSenderReaded: currentMessage.isSenderReaded ?? false,
            IsReceiverReaded: currentMessage.isReceiverReaded ?? false,
            SenderMessage: currentMessage.senderMessage ?? "",
            ReceiverMessage: currentMessage.receiverMessage ?? "",

        },
        validationSchema: schema,
        onSubmit: (answerMessage) => {
            console.log("tıkladın");
            fetch("/api/Messages/Send", {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(answerMessage)
            })
                .then(resp => resp.json())
                .then(data => setResult(data))
        }
    });
    const { errors, touched, handleSubmit, getFieldProps } = formik;

    return (

        <Page title="Mesajlar | My Apartments">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h3" gutterBottom>
                        #{currentMessage.id} Numaralı Mesaj
                    </Typography>
                </Stack>

                <Stack spacing={2} mb={3}>
                    {result.isSuccess === true &&
                        <Alert severity="success">Mesaj başarıyla iletildi</Alert>
                    }
                    {result.isSuccess === false && <Alert severity='error'>{result.exeptionMessage}</Alert>
                    }

                </Stack>
                <Stack spacing={3}>
                    <Stack direction="row" alignItems="center" justifyContent="center" mb={5}>
                        <Typography variant="h4" gutterBottom>
                            {currentMessage.messageTitle}
                        </Typography>
                    </Stack>


                    <Stack alignItems="flex-start" justifyContent="center">
                        <Card sx={{ minWidth: 300 }}>
                            <CardContent>
                                <Typography variant="h5">
                                    {currentMessage.senderMessage}
                                </Typography>
                                {currentMessage.insertDate}
                            </CardContent>
                        </Card>
                    </Stack>
                    {isFinished && (
                        <Stack alignItems="flex-end" justifyContent="center">
                            <Card sx={{ minWidth: 300 }}>
                                <CardContent>
                                    <Typography variant="h5">
                                        {currentMessage?.receiverMessage}
                                    </Typography>
                                    {currentMessage?.updateDate}
                                </CardContent>
                            </Card>
                        </Stack>
                    )}


                </Stack>
                {(/*!isFinished || */!isSender) && ( (!isFinished) &&(
                    <FormikProvider value={formik}>
                        <Form onSubmit={handleSubmit}>
                            <Stack spacing={3} my={5}>
                                <TextField
                                    fullWidth
                                    label="Cevapınız"
                                    name="ReceiverMessage"
                                    {...getFieldProps('ReceiverMessage')}
                                    error={Boolean(touched.ReceiverMessage && errors.ReceiverMessage)}
                                    helperText={touched.ReceiverMessage && errors.ReceiverMessage}
                                />
                                <Button
                                    color="warning"
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
                ))}

            </Container>
        </Page>
    )
}
