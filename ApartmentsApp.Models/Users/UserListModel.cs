using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Users
{
    public class UserListModel
    {
        public int Id { get; set; }
        public string TcNo { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime InsertDate { get; set; }

        //varsa kaldığı evin daire numarası
        public short? DoorNumber { get; set; }
    }
}
