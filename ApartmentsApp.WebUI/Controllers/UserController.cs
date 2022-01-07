using ApartmentsApp.Models;
using ApartmentsApp.Models.Users;
using ApartmentsApp.Services.UserServices;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public BaseModel<UserSelectListModel> PopulateUserDropdown()
        {
            int id = 5;//bu kısım servislerle alınacak current user idsi olacak
            BaseModel<UserSelectListModel> response = new();
            response = _userService.FillDropdownWithUsers(id);
            return response;
        }
    }
}
