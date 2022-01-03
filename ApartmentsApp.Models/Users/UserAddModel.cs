using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Users
{
    //kullanıcı kendi bilgilerini kendisi ekler sadece şifre otomatik oluşturulup dışardan gelir
    //password ve insertDate yok. onlar dışardan otomatik eklenecektir
    public class UserAddModel
    {
        public int Id { get; set; }
        public string AspNetUserId { get; set; }
        public string TcNo { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //şifre controller tarfında oluşturulup modele eklenecektir. service kısmına password dolu olarak gelecektir.
        public string Password { get; set; }
        public string CarPlate { get; set; }
    }
}
