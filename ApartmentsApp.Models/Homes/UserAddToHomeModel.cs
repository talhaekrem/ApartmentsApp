using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models.Homes
{
    public class UserAddToHomeModel
    {
        //eklenecek kullanıcının Idsi ve görünen adı
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        //eklenecek evin Idsi ve kapı numarası
        public int HomeId { get; set; }
        public short? DoorNumber { get; set; }
        //doorNumber ve DisplayNamein nullable olmasının nedeni: işlem başarılı olunca geriye yine aynı bu modeli döneceğimden
        //nullable alanları işlemin sonunda doldurup hangi evi kime eklediğini göstermek istediğimdendir.
        //ayrı bir model oluşturup onu da doldurabiliriz ama gerek yok.
    }
}
