using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Messages
{
    //mesajı yollarken kullanılan model. ilk sender dolar. sonra receiver cevap verir ve receiver dolar
    public class MessageSendModel
    {
        //ilk eklemede gelen veriler: mesajın başlığı, içeriği, mesajı yollayan ve mesajı alan
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageTitle { get; set; }
        public string SenderMessage { get; set; }

        //serviste girilen veri
        public DateTime InsertDate { get; set; }

        //değişkenlik gösterenler
        public bool IsSenderReaded { get; set; }
        public bool IsReceiverReaded { get; set; }
        public string ReceiverMessage { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
