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
        //tüm evleri listele
        BaseModel<HomeListModel> GetAll();

        BaseModel<HomeSelectListModel> GetBillableHomes();
        //idye göre (aktif pasif önemli değil) evi getir
        BaseModel<HomeDetailModel> GetHome(int id);
        //ev ekle
        BaseModel<HomeDetailModel> Add(HomeAddModel newHome);
        //ev güncelle
        BaseModel<HomeDetailModel> Update(HomeAddModel updateHome);
        //evi sil. silmekten kastım bu ev artık kiralanabilir değil. çünkü normalde bir ev silinmez. apartmanda duran daireyi nasıl silcen. isActive false yapıyorum burada
        BaseModel<bool> SetHomeEmpty(int id);
        //boş ve aktif evleri getir
        //BaseModel<HomeListModel> GetEmptyHomes();
        //sadece aktif evleri listele dataTableda bu method çalışacak
        //BaseModel<HomeListModel> GetActiveHomes();

        //kullanıcıya ev ata gibi bir method yaz. modelden gelen userId ve homeIdlere göre homes tablosundaki ownerIdyi doldur.
    }
}
