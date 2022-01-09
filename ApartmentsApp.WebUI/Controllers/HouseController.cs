using ApartmentsApp.Models;
using ApartmentsApp.Models.Homes;
using ApartmentsApp.Models.Users;
using ApartmentsApp.Services.HomeServices;
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
    //[Authorize]
    public class HouseController : ControllerBase
    {
        private readonly IHomeService _homeService;
        public HouseController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        //tüm evleri getir
        [HttpGet]
        public BaseModel<HomeListModel> GetAll()
        {
            BaseModel<HomeListModel> response = new();
            response = _homeService.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public BaseModel<HomeDetailsModel> GetById(int id)
        {
            BaseModel<HomeDetailsModel> response = new();
            response = _homeService.GetHome(id);
            return response;
        }

        [HttpPost]
        public BaseModel<HomeDetailsModel> InsertOrUpdate([FromBody] HomeAddModel home)
        {
            BaseModel<HomeDetailsModel> response = new();
            if (home.OwnerId == 0)
            {
                home.OwnerId = null;
            }
            if (home is { Id: > 0 })
            {
                response = _homeService.Update(home);
            }
            else
            {

                response = _homeService.Add(home);
            }
            return response;
        }

        [HttpDelete("{id}")]
        public BaseModel<bool> Delete(int id)
        {
            BaseModel<bool> response = new();
            response = _homeService.SetHomeEmpty(id);
            return response;
        }
    }
}
