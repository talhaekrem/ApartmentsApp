using ApartmentsApp.Models;
using ApartmentsApp.Models.Homes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Services.HomeServices
{
    public interface IHomeService
    {
        //tüm evleri filtresiz bir şekilde listele
        BaseModel<HomeListModel> GetAll();
        //fatura ekle kısmında evi seçeceğimiz(dropdown) select listinde sahipli evleri listelediğimiz method sahipli evleri listeler
        BaseModel<HomeSelectListModel> GetBillableHomes();
        //idye göre (aktif, pasif, sahipli, sahipsiz önemli değil) evi getir
        BaseModel<HomeDetailsModel> GetHome(int id);
        //ev ekle
        BaseModel<HomeDetailsModel> Add(HomeAddModel newHome);
        //ev güncelle
        BaseModel<HomeDetailsModel> Update(HomeAddModel updateHome);
        //evi sil. silmekten kastım bu ev artık kiralanabilir değil. çünkü normalde bir ev silinmez. apartmanda duran daireyi nasıl silcen. isActive false yapıyorum burada
        BaseModel<bool> SetHomeEmpty(int id);
        //boş ve aktif evleri getir
        //BaseModel<HomeListModel> GetEmptyHomes();
        //sadece aktif evleri listele dataTableda bu method çalışacak
        //BaseModel<HomeListModel> GetActiveHomes();
        //kullanıcıya ev ata gibi bir method yaz. modelden gelen userId ve homeIdlere göre homes tablosundaki ownerIdyi doldur.
        BaseModel<UserAddToHomeModel> AddUserToHome(UserAddToHomeModel userNhome);
    }
}
