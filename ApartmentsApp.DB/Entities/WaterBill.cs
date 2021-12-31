﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ApartmentsApp.DB.Entities
{
    public partial class WaterBill
    {
        public int Id { get; set; }
        public int BillsId { get; set; }
        public bool IsPaid { get; set; }
        public decimal WaterPrice { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual Bills Bills { get; set; }
    }
}
