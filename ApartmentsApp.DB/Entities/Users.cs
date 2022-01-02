using System;
using System.Collections.Generic;

#nullable disable

namespace ApartmentsApp.DB.Entities
{
    public partial class Users
    {
        public Users()
        {
            Homes = new HashSet<Homes>();
            MessagesReceiver = new HashSet<Messages>();
            MessagesSender = new HashSet<Messages>();
        }

        public int Id { get; set; }
        public string AspNetUserId { get; set; }
        public string TcNo { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string CarPlate { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ICollection<Homes> Homes { get; set; }
        public virtual ICollection<Messages> MessagesReceiver { get; set; }
        public virtual ICollection<Messages> MessagesSender { get; set; }
    }
}
