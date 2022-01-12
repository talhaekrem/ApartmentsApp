using ApartmentsApp.Models.Users;
using ApartmentsApp.Services.UserServices;
using ApartmentsApp.WebUI.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApartmentsApp.Models;
using System.Security.Claims;

namespace ApartmentsApp.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;
        public AccountController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("Login")]
        public BaseModel<UserAuthModel> Login([FromBody] UserLoginModel login)
        {
            BaseModel<UserAuthModel> response = new() { isSuccess = false };
            UserAuthModel model = new()
            {
                isLogged = false,
                isAdmin = false
            };
            response.entity = model;
            var user = _userService.GetByTcNo(login.TcNo);
            //veritabanında bu tcye ait kullanıcı yoksa
            if (user.entity == null)
            {
                response.exeptionMessage = "Tc Kimlik No Hatalı";
            }
            else
            {
                //şifre yanlışsa
                if (!BCrypt.Net.BCrypt.Verify(login.Password, user.entity.Password))
                {
                    response.exeptionMessage = "Şifre Hatalı";
                }
                else
                {
                    //hem kullanıcı adı hem de şifre doğru. jwt oluşturup cookie içine atıyoruz
                    var jwt = _jwtService.GenerateToken(user.entity.Id, user.entity.IsAdmin);
                    Response.Cookies.Append("jwt", jwt, new CookieOptions
                    {
                        HttpOnly = true
                    });
                    response.isSuccess = true;
                    response.entity.isLogged = true;
                    response.entity.isAdmin = user.entity.IsAdmin;
                }
            }
            return response;
        }

        [HttpGet]
        [Route("Verify")]
        public BaseModel<UserAuthModel> VerifyUser()
        {
            BaseModel<UserAuthModel> response = new() { isSuccess = false };
            UserAuthModel model = new()
            {
                isAdmin = false,
                isLogged = false
            };
            response.entity = model;
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return response;
            }
            else
            {
                response.isSuccess = true;
                var token = _jwtService.Verify(jwt);
                response.entity.isLogged = true;
                var test = token.Claims.FirstOrDefault(c => c.Type == "role");
                if (test.Value == "Admin")
                {
                    response.entity.isAdmin = true;
                }
            }

            return response;
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "success"
            });
        }
    }
}
