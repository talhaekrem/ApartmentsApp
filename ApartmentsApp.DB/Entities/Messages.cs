using System;
using System.Collections.Generic;

#nullable disable

namespace ApartmentsApp.DB.Entities
{
    public partial class Messages
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public bool IsSenderReaded { get; set; }
        public bool IsReceiverReaded { get; set; }
        public string MessageTitle { get; set; }
        public string SenderMessage { get; set; }
        public string ReceiverMessage { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Users Receiver { get; set; }
        public virtual Users Sender { get; set; }
    }
}
