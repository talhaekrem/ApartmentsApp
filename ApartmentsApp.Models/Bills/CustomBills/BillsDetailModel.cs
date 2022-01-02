using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Bills.CustomBills
{
    public class BillsDetailModel
    {
        public int Id { get; set; }
        public int BillsId { get; set; }
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
