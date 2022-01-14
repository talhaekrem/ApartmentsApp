using ApartmentsApp.DB.Entities.ApartmentsAppDbContext;
using ApartmentsApp.Models;
using ApartmentsApp.Models.Messages;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Services.MessageServices
{
    public class MessageManager : IMessageService
    {
        private readonly IMapper _mapper;
        public MessageManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public BaseModel<MessageDetailModel> GetById(int messageId)
        {
            var result = new BaseModel<MessageDetailModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var message = _context.Messages.FirstOrDefault(m => m.Id == messageId);

                if (message is not null)
                {
                    result.entity = _mapper.Map<MessageDetailModel>(message);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Girdiğiniz idye ait ev mesaj bulunmamaktadır.";
                }
            }
            return result;
        }

        public BaseModel<MessageListModel> ListMyMessages(int MyId)
        {
            var result = new BaseModel<MessageListModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var messages = _context.Messages.Where(m => m.ReceiverId == MyId || m.SenderId == MyId);
                if (messages.Any())
                {
                    var list = _mapper.Map<List<MessageListModel>>(messages);
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].SenderDisplayName = _context.Users.FirstOrDefault(u => u.Id == list[i].SenderId).DisplayName;
                        list[i].ReceiverDisplayName = _context.Users.FirstOrDefault(u => u.Id == list[i].ReceiverId).DisplayName;
                    }
                    result.entityList = list;
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Mesaj kutunuz boş :(";
                }
            }
            return result;
        }

        public BaseModel<MessageSendModel> SendMessage(MessageSendModel message)
        {
            var result = new BaseModel<MessageSendModel>() { isSuccess = false };
            var model = _mapper.Map<ApartmentsApp.DB.Entities.Messages>(message);
            using (var _context = new ApartmentsAppContext())
            {
                //bu durumda mesaj ilk kez oluşturulmuş demektir. yani senderı işleyeceğiz.
                if (message.ReceiverMessage == null)
                {
                    model.IsSenderReaded = true;
                    model.InsertDate = DateTime.Now;
                    _context.Messages.Add(model);
                    _context.SaveChanges();
                    result.isSuccess = true;
                    result.entity = _mapper.Map<MessageSendModel>(model);
                }
                else//bu kısım aslında update kısmı. yani receiver aynı satıra ekleme yapacak. receiverMessage işlenecektir.
                {
                    var msg = _context.Messages.FirstOrDefault(m => m.Id == message.Id);
                    model.InsertDate = msg.InsertDate;
                    model.UpdateDate = DateTime.Now;

                    //bana gelen mesaja cevap verdim. yani mesaj ben tarafından görüldü ve yolladığım kişi henüz görmedi.
                    //ben alıcıyım ve mesajı gördüm İsReveiverReaded true. yolladığım kişi henüz mesajı görmedi. IsSenderReaded false.
                    model.IsReceiverReaded = true;
                    model.IsSenderReaded = false;
                    _context.Entry(msg).CurrentValues.SetValues(model);
                    _context.SaveChanges();
                    result.isSuccess = true;
                    result.entity = _mapper.Map<MessageSendModel>(model);
                }
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Mesaj yolanırken bir hata oluştu.";
            }
            return result;
        }

        public void SetReceiverReaded(int messageId)
        {
            using (var _context = new ApartmentsAppContext())
            {
                var message = _context.Messages.FirstOrDefault(m => m.Id == messageId);
                message.IsReceiverReaded = true;
                _context.Messages.Update(message);
                _context.SaveChanges();
            }
        }

        public void SetSenderReaded(int messageId)
        {
            using (var _context = new ApartmentsAppContext())
            {
                var message = _context.Messages.FirstOrDefault(m => m.Id == messageId);
                message.IsSenderReaded = true;
                _context.Messages.Update(message);
                _context.SaveChanges();
            }
        }
    }
}
