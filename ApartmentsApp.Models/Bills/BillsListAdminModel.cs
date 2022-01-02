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

        //ayrıyetten her fatura tipinin ödenip ödenmediği bilgisini alıyorum. 
        //eğer null hatası alırsak hepsini nullable yap. boolda null geliyor mu ondan da emin değilim.
        public bool IsHomeBillPaid { get; set; }
        public bool IsElectricBillPaid { get; set; }
        public bool IsWaterBillPaid { get; set; }
        public bool IsGasBillPaid { get; set; }
        /*
         
         Employee kaynak(src), EmployeeDto hedef(dest)
         Mapper.CreateMap<Employee, EmployeeDto>()
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name));
         */
    }
}
