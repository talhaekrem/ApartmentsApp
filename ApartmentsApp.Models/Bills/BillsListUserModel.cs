using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Bills
{
    //kullanıcıların görüntüleyeceği fatura tablosudur.
    //adminin görüntülediği tablodan farklı olarak ev bilgisi yok ve faturaların son ödenme tarihi olacaktır.
    //hepsini nullable yapıyorum çünkü henüz girilmemiş bir fatura olabilir. hepsinin(kira,su,doğalgaz,elektrik)
    //kesim tarihi aynı olmayabilir. o yüzden bir sütunda veri yoksa heniz faturası girilmemiş demek oluyor.
    public class BillsListUserModel
    {
        //fatura id
        public int Id { get; set; }

        //ev kirası
        public decimal? HomePrice { get; set; }
        public DateTime? HomeBillDate { get; set; }

        //elektrik faturası
        public decimal? ElectricPrice { get; set; }
        public DateTime? ElectricBillDate { get; set; }

        //su faturası
        public decimal? WaterPrice { get; set; }
        public DateTime? WaterBillDate { get; set; }

        //doğalgaz faturası
        public decimal? GasPrice { get; set; }
        public DateTime? GasBillDate { get; set; }
    }
}
