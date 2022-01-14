using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Bills.CustomBills
{
    public class BillsAddMultipleModel
    {
        public int Id { get; set; }
        public int? HomeId { get; set; }

        public bool IsEveryone { get; set; }

        public bool Electric { get; set; }
        public decimal ElectricPrice { get; set; }
        public DateTime ElectricBillDate { get; set; }

        public bool Water { get; set; }
        public decimal WaterPrice { get; set; }
        public DateTime WaterBillDate { get; set; }

        public bool Gas { get; set; }
        public decimal GasPrice { get; set; }
        public DateTime GasBillDate { get; set; }

        public bool Dues { get; set; }
    }
}
