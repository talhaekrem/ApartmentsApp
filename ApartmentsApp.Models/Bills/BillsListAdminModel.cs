using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Bills
{
    //yöneticinin göreceği faturaların listesi. yönetici hangi evin hangi faturaları ödediği-ödemediğini görür
    public class BillsListAdminModel
    {
        //bills tablosundaki id ve ev idyi alıyorum. kullanıcı tablosunda ev id olmayacak.
        public int Id { get; set; }
        public int HomeId { get; set; }

        //ayrıyetten her fatura tipinin ödenip ödenmediği veya fatura kesildi mi bilgisini alıyorum. 
        public bool IsHomeBillPaid { get; set; }
        public bool HomeBillActive { get; set; }

        public bool IsElectricBillPaid { get; set; }
        public bool ElectricBillActive { get; set; }

        public bool IsWaterBillPaid { get; set; }
        public bool WaterBillActive { get; set; }

        public bool IsGasBillPaid { get; set; }
        public bool GasBillActive { get; set; }

    }
}
