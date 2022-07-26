﻿using ApartmentsApp.Models;
using ApartmentsApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Services.UserServices
{
    public interface IUserService
    {
        //tüm kullanıcıları getir. filtresiz
        BaseModel<UserListModel> GetAll();
        //idye göre getir
        BaseModel<UserDetailsModel> GetById(int id);
        //tcnoya göre kullanıcıyı getir. login işlemi için
        BaseModel<UserDetailsModel> GetByTcNo(string tcNo);
        //kullanıcı kayıt ol(ekle)
        BaseModel<UserDetailsModel> Add(UserAddModel addUser);
        //kullanıcıyı güncelle
        BaseModel<UserDetailsModel> Update(UserUpdateModel updateUser);
        //kullanıcıyı devre dışı bırak
        BaseModel<bool> Delete(int id);

        BaseModel<AccountDetailsModel> GetMyDetails(int id);

        BaseModel<UserSelectListModel> FillDropdownWithUsers();
        BaseModel<UserSelectListModel> FillDropdownWithAdmins();

    }
}
