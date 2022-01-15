using ApartmentsApp.Core.Bills;
using ApartmentsApp.Models;
using ApartmentsApp.Models.Bills;
using ApartmentsApp.Models.Bills.CustomBills;
using ApartmentsApp.Models.Homes;
using ApartmentsApp.Services.BillServices;
using ApartmentsApp.Services.BillServices.CustomBill;
using ApartmentsApp.WebUI.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsApp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillUserController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly ICustomBillService _customBillService;
        private readonly JwtService _jwtService;
        public BillUserController(IBillService billService, ICustomBillService customBillService, JwtService jwtService)
        {
            _billService = billService;
            _customBillService = customBillService;
            _jwtService = jwtService;
        }

        [HttpGet]
        public BaseModel<BillsListUserModel> GetAll()
        {
            BaseModel<BillsListUserModel> response = new();
            var token = _jwtService.Verify(Request.Cookies["jwt"]);
            int currentUserId = Convert.ToInt32(token.Claims.FirstOrDefault(c => c.Type == "unique_name").Value);
            response = _billService.GetAllAsUser(currentUserId);
            return response;
        }

        [HttpGet("detail/{type}/{billId}")]
        public BaseModel<BillsDetailsModel> GetById(string type, int billId)
        {
            BaseModel<BillsDetailsModel> response = new();
            switch (type)
            {
                case "dues":
                    response = _customBillService.GetBillDetails(billId, BillType.Home);
                    break;
                case "electric":
                    response = _customBillService.GetBillDetails(billId, BillType.Electric);
                    break;
                case "water":
                    response = _customBillService.GetBillDetails(billId, BillType.Water);

                    break;
                case "gas":
                    response = _customBillService.GetBillDetails(billId, BillType.Gas);
                    break;
                default:
                    response.exeptionMessage = "Bir hata oluþtu. Yöneticinize danýþýn";
                    break;
            }
            return response;
        }

        [HttpGet("pay/{type}/{billId}")]
        public bool PayBill(string type, int billId)
        {
            bool response = false;
            switch (type)
            {
                case "dues":
                    response = _customBillService.PayBill(billId, BillType.Home);
                    break;
                case "electric":
                    response = _customBillService.PayBill(billId, BillType.Electric);
                    break;
                case "water":
                    response = _customBillService.PayBill(billId, BillType.Water);

                    break;
                case "gas":
                    response = _customBillService.PayBill(billId, BillType.Gas);
                    break;
                default:
                    break;
            }
            return response;
        }
    }
}
