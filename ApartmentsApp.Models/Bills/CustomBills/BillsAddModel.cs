using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Bills.CustomBills
{
    public class BillsAddModel
    {
        public int Id { get; set; }
        public int? BillsId { get; set; }
        public decimal Price { get; set; }
        public DateTime BillDate { get; set; }

        //bills tablosunda kayıtlı fatura var mı yok mu kontrolü için bills tablosundaki homeIdyi buraya alıyorum
        public int HomeId { get; set; }

    }
}
