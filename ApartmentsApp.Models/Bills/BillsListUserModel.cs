using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Bills
{
    public class BillsListUserModel
    {
        //fatura id
        public int Id { get; set; }

        //aidat
        public bool IsHomeBillPaid { get; set; }
        public bool HomeBillActive { get; set; }
        //elektrik
        public bool IsElectricBillPaid { get; set; }
        public bool ElectricBillActive { get; set; }
        //su
        public bool IsWaterBillPaid { get; set; }
        public bool WaterBillActive { get; set; }
        //doğalgaz
        public bool IsGasBillPaid { get; set; }
        public bool GasBillActive { get; set; }
    }
}
