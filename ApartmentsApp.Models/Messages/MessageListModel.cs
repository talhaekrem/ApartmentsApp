using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Messages
{
    public class MessageListModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public bool IsSenderReaded { get; set; }
        public bool IsReceiverReaded { get; set; }
        public string MessageTitle { get; set; }
        public DateTime InsertDate { get; set; }

        public string SenderNameSurName { get; set; }
        public string ReceiverNameSurName { get; set; }
    }
}
