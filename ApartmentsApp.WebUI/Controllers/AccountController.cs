using ApartmentsApp.Models.Users;
using ApartmentsApp.Services.UserServices;
using ApartmentsApp.WebUI.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Login([FromBody] UserLoginModel login)
        {
            var user = _userService.GetByTcNo(login.TcNo);

            //veritabanında bu tcye ait kullanıcı yoksa
            if (user.entity == null)
            {
                return BadRequest(new { message = "Geçersiz kimlik bilgileri" });
            }

            //şifre yanlışsa
            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.entity.Password))
            {
                return BadRequest(new { message = "Şifre hatalı" });
            }

            var jwt = _jwtService.GenerateToken(user.entity.Id, user.entity.IsAdmin);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new { message = "success" });
        }

        //[HttpGet]
        //[Route("User")]
        //public IActionResult GetUser()
        //{
        //    try
        //    {
        //        var jwt = Request.Cookies["jwt"];
        //        //kullanıcının cookilerinde login olduğunda aldığı tokenı sistemdekiyle eşleştirip kontrol ediyoruz
        //        var token = _jwtService.Verify(jwt);
        //        int userId = int.Parse(token.Issuer);

        //        var user = _userService.GetById(userId);
        //        return Ok(user);
        //    }
        //    catch (Exception)
        //    {
        //        return Unauthorized();
        //    }
        //}

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
