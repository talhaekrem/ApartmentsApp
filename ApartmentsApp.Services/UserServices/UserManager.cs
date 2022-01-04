using ApartmentsApp.DB.Entities.ApartmentsAppDbContext;
using ApartmentsApp.Models;
using ApartmentsApp.Models.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Services.UserServices
{
    public class UserManager : IUserService
    {
        private readonly IMapper _mapper;
        public UserManager(IMapper mapper)
        {
            _mapper = mapper;
        }
        public BaseModel<UserDetailsModel> Add(UserAddModel addUser)
        {
            var result = new BaseModel<UserDetailsModel>() { isSuccess = false };
            var model = _mapper.Map<ApartmentsApp.DB.Entities.Users>(addUser);
            using (var _context = new ApartmentsAppContext())
            {
                model.InsertDate = DateTime.Now;
                _context.Users.Add(model);
                _context.SaveChanges();
                result.isSuccess = true;
                result.entity = _mapper.Map<UserDetailsModel>(model);
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Kayıt işleminde bir hata oluştu.";
            }
            return result;
        }

        public BaseModel<bool> Delete(int id)
        {
            var result = new BaseModel<bool>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                user.IsDeleted = false;
                _context.SaveChanges();
                result.isSuccess = true;
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Kullanıcı silinirken bir hata oluştu.";
            }
            return result;
        }

        //mevcut kullanıcının idsini almamın sebebi: listede kendisi de gelmesin diye
        public BaseModel<UserSelectListModel> FillDropdownWithUsers(int currentUserId)
        {
            var result = new BaseModel<UserSelectListModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var query = from user in _context.Users
                            where user.IsDeleted == false && user.Id != currentUserId
                            select new
                            {
                                Id = user.Id,
                                Name = user.Name,
                                SurName = user.SurName
                            };
                if (query.Any())
                {
                    result.entityList = _mapper.Map<List<UserSelectListModel>>(query);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Kullanıcı bulunmadı.";
                }
            }
            return result;
        }

        public BaseModel<UserListModel> GetAll()
        {
            var result = new BaseModel<UserListModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var users = _context.Users;

                if (users.Any())
                {
                    result.entityList = _mapper.Map<List<UserListModel>>(users);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Listede kullanıcı bulunmamaktadır.";
                }
            }
            return result;
        }

        public BaseModel<UserDetailsModel> GetById(int id)
        {
            var result = new BaseModel<UserDetailsModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                if (user is not null)
                {
                    result.entity = _mapper.Map<UserDetailsModel>(user);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Girdiğiniz idye ait kullanıcı bulunmamaktadır.";
                }
            }
            return result;
        }

        // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //arayüz tarafında asp net identity ile login olmuş kullanıcının ClaimTypelarından NameIdentifier özelliğiyle aspNetUserIdsini alabiliyoruz
        //Bu id ile kendi Users tablomuzdaki userı alıyoruz. Çünkü kullanacağımız veriler hep kendi User tablomuzda.
        // Bu sebepten hangi kullanıcı login olmuş ve işlem yaptığını bu methodla öğreneceğiz.
        public int GetCurrentUserId(string AspNetUserId)
        {
            using (var _context = new ApartmentsAppContext())
            {
                int userId = _context.Users.FirstOrDefault(u => u.AspNetUserId == AspNetUserId).Id;
                return userId;
            }
        }

        public BaseModel<UserDetailsModel> Update(UserUpdateModel updateUser)
        {
            var result = new BaseModel<UserDetailsModel>() { isSuccess = false };
            var model = _mapper.Map<ApartmentsApp.DB.Entities.Users>(updateUser);
            using (var _context = new ApartmentsAppContext())
            {
                var user = _context.Users.FirstOrDefault(h => h.Id == updateUser.Id);
                //eski veri sıfırlanmasın diye güncel veriyi tekrar işliyorum. çünkü mapledikten sonra model.insertDate alakasız bir tarih oluyor.
                model.InsertDate  = user.InsertDate;
                model.UpdateDate = DateTime.Now;
                _context.Entry(user).CurrentValues.SetValues(model);
                _context.SaveChanges();
                result.isSuccess = true;
                result.entity = _mapper.Map<UserDetailsModel>(user);
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Kullanıcı güncellenirken bir hata oluştu.";
            }
            return result;
        }
    }
}
