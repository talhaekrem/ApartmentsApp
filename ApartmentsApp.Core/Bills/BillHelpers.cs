using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Core.Bills
{
    public static class BillHelpers
    {
        public static bool IsNextMonth(DateTime billDate)
        {
            //eğer fatura kesim tarihinin üzerinden 1 ay(30 gün) geçmişse artık yeni fatura kesilme zamanı gelmiştir ve bu kullanıcının
            //bulunduğumuz ay için faturası henüz kesilmemiş demektir. aradığımız cevap ise faturası olmayan kullanıcılar olduğu için
            //true dönüyoruz. yılın kaçıncı gününde olduğumuzu aldığımızda bu değere göre kolaylıkla karşılaştırma yapabilmekteyiz.
            
            /*örnek: elektrik faturası 15 nisanda kesildi. 15 nisan yılın 105. günü yapar(fazlalık yıl değilse). Fatura bir sonraki aya(30 gün) kadar
            ödenmelidir. Çünkü 15 mayısta yeni fatura kesilecektir. 15 mayıs ise yılın 135. günüdür. EĞER şuanki yılın günü, faturanın kesildiği 
            yılın gününün üzerine 30 gün eklenmiş halinden büyükse artık sen bir sonraki aydasındır ve yeni fatura kesebilirsindir.
            Çok basit, işe yarar pratik, hayat kurtaran bir method oldu. Düşünürken beynim yandı. 
             */
            if (billDate.DayOfYear + 30 < DateTime.Now.DayOfYear)
            {
                return true;
            }
            return false;
        }

    }
}
