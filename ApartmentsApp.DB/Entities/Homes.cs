using System;
using System.Collections.Generic;

#nullable disable

namespace ApartmentsApp.DB.Entities
{
    public partial class Homes
    {
        public Homes()
        {
            Bills = new HashSet<Bills>();
        }

        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public bool IsOwned { get; set; }
        public bool IsActive { get; set; }
        public string BlockName { get; set; }
        public string HomeType { get; set; }
        public short FloorNumber { get; set; }
        public short DoorNumber { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public decimal DuesPrice { get; set; }

        public virtual Users Owner { get; set; }
        public virtual ICollection<Bills> Bills { get; set; }
    }
}
