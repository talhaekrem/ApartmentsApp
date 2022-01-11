using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Homes
{
    public class HomeListModel
    {
        public int Id { get; set; }
        public string OwnerDisplayName { get; set; }
        public bool IsActive { get; set; }
        public string BlockName { get; set; }
        public short FloorNumber { get; set; }
        public short DoorNumber { get; set; }
    }
}
