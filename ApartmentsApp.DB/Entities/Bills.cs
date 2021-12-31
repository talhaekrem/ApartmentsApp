using System;
using System.Collections.Generic;

#nullable disable

namespace ApartmentsApp.DB.Entities
{
    public partial class Bills
    {
        public Bills()
        {
            ElectricBill = new HashSet<ElectricBill>();
            GasBill = new HashSet<GasBill>();
            HomeBill = new HashSet<HomeBill>();
            WaterBill = new HashSet<WaterBill>();
        }

        public int Id { get; set; }
        public int HomeId { get; set; }

        public virtual Homes Home { get; set; }
        public virtual ICollection<ElectricBill> ElectricBill { get; set; }
        public virtual ICollection<GasBill> GasBill { get; set; }
        public virtual ICollection<HomeBill> HomeBill { get; set; }
        public virtual ICollection<WaterBill> WaterBill { get; set; }
    }
}
