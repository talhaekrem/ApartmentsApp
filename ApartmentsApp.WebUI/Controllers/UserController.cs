﻿using ApartmentsApp.BackgroundJobs.MailSender;
using ApartmentsApp.Core.Users;
using ApartmentsApp.Models;
using ApartmentsApp.Models.Users;
using ApartmentsApp.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsApp.WebUI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public BaseModel<UserListModel> GetAll()
        {
            BaseModel<UserListModel> response = new();
            response = _userService.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public BaseModel<UserDetailsModel> GetById(int id)
        {
            BaseModel<UserDetailsModel> response = new();
            response = _userService.GetById(id);
            return response;
        }

        [HttpPost]
        [Obsolete]
        public BaseModel<UserDetailsModel> Insert([FromBody] UserAddModel newUser)
        {
            BaseModel<UserDetailsModel> response = new();
            //formdan tcno,ad,soyad,telefon,eposta,rol,plaka bilgilerini aldık
            //id otomatik oluşuyor. display namei burada setledik random şifre generate ettik, insert date ise servis kısmında giriliyor.
            newUser.CarPlate = newUser.CarPlate == "" ? null : newUser.CarPlate;
            newUser.DisplayName = string.Format("{0} {1}", newUser.Name, newUser.SurName);
            //random şifre oluştur ve şifrele. Bu şifreyen adminin asla haberi olmayacak
            string generatedPassword = UserHelpers.GenerateRandomPassword();

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(generatedPassword);
            response = _userService.Add(newUser);
            if (response.isSuccess)
            {
                //hangfire:random password oluşturulunca bunu kullanıcıya mail olarak iletiyorum daha sonra şifreleyip veritabanına kaydediyorum
                SendPasswordJob.SendUsersPassword(newUser.Email, newUser.DisplayName, generatedPassword);
                //hangfire
            }
            return response;
        }

        [HttpPut]
        public BaseModel<UserDetailsModel> Update([FromBody] UserUpdateModel updateUser)
        {
            BaseModel<UserDetailsModel> response = new();
            updateUser.CarPlate = updateUser.CarPlate == "" ? null : updateUser.CarPlate;
            response = _userService.Update(updateUser);
            return response;
        }

        [HttpDelete("{id}")]
        public BaseModel<bool> Delete(int id)
        {
            BaseModel<bool> response = new();
            response = _userService.Delete(id);
            return response;
        }

        [HttpGet]
        [Route("PopulateList")]
        public BaseModel<UserSelectListModel> PopulateUserDropdown()
        {
            BaseModel<UserSelectListModel> response = new();
            response = _userService.FillDropdownWithUsers();
            return response;
        }
    }
}
