﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Homes
{
    public class HomeDetailsModel
    {
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

    }
}
