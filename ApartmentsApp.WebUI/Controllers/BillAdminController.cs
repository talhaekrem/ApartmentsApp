using ApartmentsApp.Core.Bills;
using ApartmentsApp.Models;
using ApartmentsApp.Models.Bills;
using ApartmentsApp.Models.Bills.CustomBills;
using ApartmentsApp.Models.Homes;
using ApartmentsApp.Services.BillServices;
using ApartmentsApp.Services.BillServices.CustomBill;
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
    [Authorize(Roles = "Admin")]
    public class BillAdminController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly ICustomBillService _customBillService;
        public BillAdminController(IBillService billService, ICustomBillService customBillService)
        {
            _billService = billService;
            _customBillService = customBillService;
        }

        [HttpGet]
        public BaseModel<BillsListAdminModel> GetAll()
        {
            BaseModel<BillsListAdminModel> response = new();
            response = _billService.GetAllAsAdmin();
            return response;
        }

        [HttpPost]
        public BaseModel<BillsDetailsModel> Insert([FromBody] BillsAddMultipleModel model)
        {
            BaseModel<BillsDetailsModel> response = new();
            response.entityList = new();
            if (model.Dues)
            {
                BillsAddModel dues = new()
                {
                    BillDate = model.DuesBillDate,
                    BillsId = model.Id,
                    HomeId = model.HomeId,
                    Price = model.DuesPrice
                };
                response.entityList.Add(_customBillService.AddBill(dues, BillType.Home).entity);
            }
            if (model.Electric)
            {
                BillsAddModel electric = new()
                {
                    BillDate = model.ElectricBillDate,
                    BillsId = model.Id,
                    HomeId = model.HomeId,
                    Price = model.ElectricPrice
                };
                response.entityList.Add(_customBillService.AddBill(electric, BillType.Electric).entity);
            }
            if (model.Water)
            {
                BillsAddModel water = new()
                {
                    BillDate = model.WaterBillDate,
                    BillsId = model.Id,
                    HomeId = model.HomeId,
                    Price = model.WaterPrice
                };
                response.entityList.Add(_customBillService.AddBill(water, BillType.Water).entity);
            }
            if (model.Gas)
            {
                BillsAddModel gas = new()
                {
                    BillDate = model.GasBillDate,
                    BillsId = model.Id,
                    HomeId = model.HomeId,
                    Price = model.GasPrice
                };
                response.entityList.Add(_customBillService.AddBill(gas, BillType.Gas).entity);
            }
            response.isSuccess = true;
            return response;
        }

        [HttpPost("AddOne/{type}/{billId}")]
        public BaseModel<BillsDetailsModel> InsertOne([FromBody] BillsAddModel model, string type, int billId)
        {
            BaseModel<BillsDetailsModel> response = new();
            switch (type)
            {
                case "dues":
                    response = _customBillService.InsertOneBill(model, BillType.Home, billId);
                    break;
                case "electric":
                    response = _customBillService.InsertOneBill(model, BillType.Electric, billId);

                    break;
                case "water":
                    response = _customBillService.InsertOneBill(model, BillType.Water, billId);

                    break;
                case "gas":
                    response = _customBillService.InsertOneBill(model, BillType.Gas, billId);
                    break;
                default:
                    response.exeptionMessage = "Bir hata oluştu. Yöneticinize danışın";
                    break;
            }
            return response;
        }

        [HttpGet("{type}/{billId}")]
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
                    response.exeptionMessage = "Bir hata oluştu. Yöneticinize danışın";
                    break;
            }
            return response;
        }

        [HttpPut("{type}")]
        public BaseModel<BillsDetailsModel> Update(BillsUpdateModel updateBill, string type)
        {
            BaseModel<BillsDetailsModel> response = new();

            switch (type)
            {
                case "dues":
                    response = _customBillService.UpdateBill(updateBill, BillType.Home);
                    break;
                case "electric":
                    response = _customBillService.UpdateBill(updateBill, BillType.Electric);
                    break;
                case "water":
                    response = _customBillService.UpdateBill(updateBill, BillType.Water);
                    break;
                case "gas":
                    response = _customBillService.UpdateBill(updateBill, BillType.Gas);
                    break;
                default:
                    break;
            }
            return response;
        }
    
        [HttpDelete("{type}/{billId}")]
        public BaseModel<bool> Delete(string type, int billId)
        {
            BaseModel<bool> response = new();

            switch (type)
            {
                case "dues":
                    response = _customBillService.DeleteBill(billId, BillType.Home);
                    break;
                case "electric":
                    response = _customBillService.DeleteBill(billId, BillType.Electric);
                    break;
                case "water":
                    response = _customBillService.DeleteBill(billId, BillType.Water);
                    break;
                case "gas":
                    response = _customBillService.DeleteBill(billId, BillType.Gas);
                    break;
                default:
                    break;
            }
            return response;
        }
    }
}
