using ApartmentsApp.DB.Entities.ApartmentsAppDbContext;
using ApartmentsApp.Models;
using ApartmentsApp.Models.Homes;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Services.HomeServices
{
    public class HomeManager : IHomeService
    {
        private readonly IMapper _mapper;
        public HomeManager(IMapper mapper)
        {
            _mapper = mapper;
        }
        public BaseModel<HomeDetailModel> Add(HomeAddModel newHome)
        {
            var result = new BaseModel<HomeDetailModel>() { isSuccess = false };
            var model = _mapper.Map<ApartmentsApp.DB.Entities.Homes>(newHome);
            using (var _context = new ApartmentsAppContext())
            {
                model.InsertDate = DateTime.Now;
                model.IsActive = true;
                _context.Homes.Add(model);
                _context.SaveChanges();
                result.isSuccess = true;
                result.entity = _mapper.Map<HomeDetailModel>(model);
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Ev eklenirken hata oluştu.";
            }
            return result;
        }

        public BaseModel<HomeDetailModel> Update(HomeAddModel updateHome)
        {
            var result = new BaseModel<HomeDetailModel>() { isSuccess = false };
            var model = _mapper.Map<ApartmentsApp.DB.Entities.Homes>(updateHome);
            using (var _context = new ApartmentsAppContext())
            {
                var home = _context.Homes.FirstOrDefault(h => h.Id == updateHome.Id);
                //eski veri sıfırlanmasın diye güncel veriyi tekrar işliyorum. çünkü mapledikten sonra model.insertDate alakasız bir tarih oluyor.
                model.InsertDate = home.InsertDate;
                model.UpdateDate = DateTime.Now;

                _context.Entry(home).CurrentValues.SetValues(model);
                _context.SaveChanges();
                result.isSuccess = true;
                result.entity = _mapper.Map<HomeDetailModel>(home);
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Ev güncellenirken hata oluştu";
            }
            return result;
        }

        //public BaseModel<HomeListModel> GetActiveHomes()
        //{
        //    var result = new BaseModel<HomeListModel>() { isSuccess = false };
        //    using (var _context = new ApartmentsAppContext())
        //    {
        //        var homes = _context.Homes;

        //        if (homes.Any())
        //        {
        //            result.entityList = _mapper.Map<List<HomeListModel>>(homes);
        //            result.isSuccess = true;
        //        }
        //        else
        //        {
        //            result.exeptionMessage = "Listede ev bulunmamaktadır. Hemen ev ekleyiniz.";
        //        }
        //    }
        //    return result;
        //}

        public BaseModel<HomeListModel> GetAll()
        {
            var result = new BaseModel<HomeListModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var homes = _context.Homes;

                if (homes.Any())
                {
                    result.entityList = _mapper.Map<List<HomeListModel>>(homes);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Listede ev bulunmamaktadır. Hemen ev ekleyiniz.";
                }
            }
            return result;
        }

        //public BaseModel<HomeListModel> GetEmptyHomes()
        //{
        //    var result = new BaseModel<HomeListModel>() { isSuccess = false };
        //    using (var _context = new ApartmentsAppContext())
        //    {
        //        var homes = _context.Homes.Where(h => h.IsActive == false);

        //        if (homes.Any())
        //        {
        //            result.entityList = _mapper.Map<List<HomeListModel>>(homes);
        //            result.isSuccess = true;
        //        }
        //        else
        //        {
        //            result.exeptionMessage = "Tüm evler doludur.";
        //        }
        //    }
        //    return result;
        //}

        public BaseModel<HomeDetailModel> GetHome(int id)
        {
            var result = new BaseModel<HomeDetailModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var homes = _context.Homes.FirstOrDefault(h => h.Id == id);

                if (homes is not null)
                {
                    result.entity = _mapper.Map<HomeDetailModel>(homes);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Girdiğiniz idye ait ev bulunmamaktadır.";
                }
            }
            return result;
        }

        public BaseModel<bool> SetHomeEmpty(int id)
        {
            var result = new BaseModel<bool>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var homes = _context.Homes.FirstOrDefault(h => h.Id == id);
                if (homes.OwnerId == null)
                {
                    homes.IsActive = false;
                    _context.SaveChanges();
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Evin sahipleri vardır. Evi silemezsiniz. Sahibi kaldırmak için evi güncelleyin.";
                }

            }
            return result;
        }

        public BaseModel<HomeSelectListModel> GetBillableHomes()
        {
            var result = new BaseModel<HomeSelectListModel> { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var query = from home in _context.Homes
                            where home.IsActive && home.IsOwned
                            join user in _context.Users
                            on home.OwnerId equals user.Id
                            select new
                            {
                                Id = home.Id,
                                OwnerName = user.DisplayName
                            };
                if (query.Any())
                {
                    result.entityList = _mapper.Map<List<HomeSelectListModel>>(query);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Fatura kesilebilir ev bulunmamaktadır. Lütfen kullanıcıları evlere atayın.";
                }
            }
            return result;
        }

        public BaseModel<UserAddToHomeModel> AddUserToHome(UserAddToHomeModel userNhome)
        {
            var result = new BaseModel<UserAddToHomeModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                //kullanıcıyı ekleyeceğimiz evi modeldeki homeIdye göre dbden alıyorum. 
                var home = _context.Homes.Where(h => h.Id == userNhome.HomeId).FirstOrDefault();
                //bu evin sahibine modeldeki UserIdyi veriyorum ve ev sahiplidir alanını true yapıyorum.
                home.OwnerId = userNhome.UserId;
                home.IsOwned = true;
                //yeni verilerle evi güncelle
                _context.Homes.Update(home);
                _context.SaveChanges();

                //geriye döneceğim modeli dolduruyorum
                UserAddToHomeModel sendModel = new() {
                    HomeId = home.Id,
                    UserId = userNhome.UserId,
                    DisplayName = _context.Users.Where(u => u.Id == userNhome.UserId).Select(x => x.DisplayName).FirstOrDefault(),
                    DoorNumber = home.DoorNumber
                };
                result.entity = sendModel;
                result.isSuccess = true;
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Kullanıcıyı eve atarken bir sorun oluştu. Daha sonra tekrar deneyin.";
            }
            return result;
        }
    }
}
