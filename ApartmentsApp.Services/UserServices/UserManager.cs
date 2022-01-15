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
                if (_context.Homes.Any(h => h.OwnerId == id))
                {
                    result.exeptionMessage = "Bu kullanıcıya ait ev bulunmaktadır. Önce evden çıkışını yapın daha sonra silin";
                }
                else
                {
                    user.IsDeleted = true;
                    _context.SaveChanges();
                    result.isSuccess = true;
                }

            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Kullanıcı silinirken bir hata oluştu.";
            }
            return result;
        }

        public BaseModel<UserSelectListModel> FillDropdownWithAdmins()
        {
            var result = new BaseModel<UserSelectListModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var query = from user in _context.Users
                            where user.IsDeleted == false && user.IsAdmin
                            select new UserSelectListModel()
                            {
                                Id = user.Id,
                                DisplayName = user.DisplayName
                            };
                if (query.Any())
                {
                    result.entityList = query.ToList();
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Kullanıcı bulunmadı.";
                }
            }
            return result;
        }

        //mevcut kullanıcının idsini almamın sebebi: listede kendisi de gelmesin diye
        public BaseModel<UserSelectListModel> FillDropdownWithUsers()
        {
            var result = new BaseModel<UserSelectListModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var query = from user in _context.Users
                            where user.IsDeleted == false && !user.IsAdmin
                            select new UserSelectListModel()
                            {
                                Id = user.Id,
                                DisplayName = user.DisplayName
                            };
                if (query.Any())
                {
                    result.entityList = query.ToList();
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
                //left join. user tablosundaki verileri getiriyorum. birde eğer varsa home tablosundaki kapı no ve blok adını alıyorum
                var users = from user in _context.Users
                            join homes in _context.Homes
                            on user.Id equals homes.OwnerId into userList
                            from homes in userList.DefaultIfEmpty()
                            select new UserListModel()
                            {
                                Id = user.Id,
                                Name = user.Name,
                                SurName = user.SurName,
                                TcNo = user.TcNo,
                                Email = user.Email,
                                PhoneNumber = user.PhoneNumber,
                                BlockName = homes.BlockName == null ? "Yok" : homes.BlockName,
                                DoorNumber = homes.DoorNumber,
                                IsAdmin = user.IsAdmin
                            };
                //var users = _context.Users;

                if (users.Any())
                {
                    result.entityList = users.ToList();
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

        public BaseModel<UserDetailsModel> GetByTcNo(string tcNo)
        {
            var result = new BaseModel<UserDetailsModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var user = _context.Users.FirstOrDefault(u => u.TcNo == tcNo);
                if (user is not null)
                {
                    result.entity = _mapper.Map<UserDetailsModel>(user);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Girdiğiniz tcye ait kullanıcı bulunmamaktadır.";
                }
            }
            return result;
        }

        public BaseModel<AccountDetailsModel> GetMyDetails(int id)
        {
            var result = new BaseModel<AccountDetailsModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                if (user is not null)
                {
                    result.entity = _mapper.Map<AccountDetailsModel>(user);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Girdiğiniz idye ait kullanıcı bulunmamaktadır.";
                }
            }
            return result;
        }

        public BaseModel<UserDetailsModel> Update(UserUpdateModel updateUser)
        {
            var result = new BaseModel<UserDetailsModel>() { isSuccess = false };
            var model = _mapper.Map<ApartmentsApp.DB.Entities.Users>(updateUser);
            using (var _context = new ApartmentsAppContext())
            {
                var user = _context.Users.FirstOrDefault(h => h.Id == updateUser.Id);
                //eski veri sıfırlanmasın diye güncel veriyi tekrar işliyorum. çünkü mapledikten sonra model.insertDate alakasız bir tarih oluyor.
                model.InsertDate = user.InsertDate;
                model.UpdateDate = DateTime.Now;
                model.Password = user.Password;
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
