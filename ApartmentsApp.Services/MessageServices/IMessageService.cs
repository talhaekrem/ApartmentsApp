using ApartmentsApp.Models;
using ApartmentsApp.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Services.MessageServices
{
    public interface IMessageService
    {
        BaseModel<MessageListModel> ListMyMessages(int myId);
        BaseModel<MessageSendModel> SendMessage(MessageSendModel message);

        //mesaj talebine cevap geldiğinde ve cevabı okuduğunda mesajı görmüş sayılırsın. ve mesajı alan kişiye görüldü bilgisi gidecektir.
        void SetSenderReaded(int messageId);

        //mesaj attığınız kişi mesajınıızı gördüğünde size okundu bilgisi gelecektir.
        void SetReceiverReaded(int messageId);
    }
}
