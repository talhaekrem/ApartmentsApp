using ApartmentsApp.DB.Entities.ApartmentsAppDbContext;
using ApartmentsApp.Models;
using ApartmentsApp.Models.Bills;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Services.BillServices
{
    public class BillManager : IBillService
    {
        public BaseModel<BillsListAdminModel> GetAllAsAdmin()
        {
            var result = new BaseModel<BillsListAdminModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                /*fatura idsiniyle 4 adet fatura tipi tablolarına foreign key olan BillsId eşitliğine göre girip isPaid bilgilerini
                 modeldeki ismiyle aynı olacak şekilde isimlendirip seçiyorum. modeldeki ismiyle aynı yapmamın sebebi map yapacağım için
                yoksa patates de diyebiliriz. "|" veya yapmamın sebebi mesela henüz doğalgaz faturası girilmemişse doğalgaz tablosunda
                BillsId olmayacak ve hata verecektir. 
                 */
                //var newq = from bills in _context.Bills
                //           join  home in _context.HomeBill
                List<BillsListAdminModel> model = new();
                for (int i = 0; i < _context.Bills.Count(); i++)
                {
                    var currentBill = _context.Bills.Skip(i).Take(1).FirstOrDefault();
                    var dues = _context.HomeBill.FirstOrDefault(d => d.BillsId == currentBill.Id);
                    var electric = _context.ElectricBill.FirstOrDefault(d => d.BillsId == currentBill.Id);
                    var water = _context.WaterBill.FirstOrDefault(d => d.BillsId == currentBill.Id);
                    var gas = _context.GasBill.FirstOrDefault(d => d.BillsId == currentBill.Id);

                    bool IsHomeBillPaid = false;
                    bool HomeBillActive = false;
                    bool IsElectricBillPaid = false;
                    bool ElectricBillActive = false;
                    bool IsWaterBillPaid = false;
                    bool WaterBillActive = false;
                    bool IsGasBillPaid = false;
                    bool GasBillActive = false;

                    if (dues != null)
                    {
                        IsHomeBillPaid = dues.IsPaid;
                        HomeBillActive = true;
                    }
                    if (electric != null)
                    {
                        IsElectricBillPaid = electric.IsPaid;
                        ElectricBillActive = true;
                    }
                    if (water != null)
                    {
                        IsWaterBillPaid = water.IsPaid;
                        WaterBillActive = true;
                    }
                    if (gas != null)
                    {
                        IsGasBillPaid = gas.IsPaid;
                        GasBillActive = true;
                    }

                    model.Add(new BillsListAdminModel()
                    {
                        Id = currentBill.Id,
                        HomeId = currentBill.HomeId,
                        IsHomeBillPaid = IsHomeBillPaid,
                        HomeBillActive = HomeBillActive,
                        IsElectricBillPaid = IsElectricBillPaid,
                        ElectricBillActive = ElectricBillActive,
                        IsWaterBillPaid = IsWaterBillPaid,
                        WaterBillActive = WaterBillActive,
                        IsGasBillPaid = IsGasBillPaid,
                        GasBillActive = GasBillActive
                    });

                };
                //var query = from home in _context.HomeBill
                //            from water in _context.WaterBill
                //            from electric in _context.ElectricBill
                //            from gas in _context.GasBill
                //            join bills in _context.Bills
                //            on home.BillsId | water.BillsId | electric.BillsId | gas.BillsId equals bills.Id
                //            select new BillsListAdminModel()
                //            {
                //                Id = bills.Id,
                //                HomeId = bills.HomeId,
                //                IsHomeBillPaid = home.IsPaid,
                //                HomeBillActive = home.Price != 0 ? true : false,
                //                IsElectricBillPaid = electric.IsPaid,
                //                ElectricBillActive = electric.Price != 0 ? true : false,
                //                IsWaterBillPaid = water.IsPaid,
                //                WaterBillActive = water.Price != 0 ? true : false,
                //                IsGasBillPaid = gas.IsPaid,
                //                GasBillActive = gas.Price != 0 ? true : false,
                //            };
                if (model.Any())
                {
                    result.entityList = model;
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Listede hiç fatura bulunmamaktadır. Hemen fatura kesmeye başlayın";
                }
            }
            return result;
        }

        public BaseModel<BillsListUserModel> GetAllAsUser()
        {
            var result = new BaseModel<BillsListUserModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                //bills tablosundaki idye göre 4 farklı fatura tablosına giriyorum ve her faturaya ait fiyat ve fatura kesim tarihini alıyorum
                var query = from home in _context.HomeBill
                            from water in _context.WaterBill
                            from electric in _context.ElectricBill
                            from gas in _context.GasBill
                            join bills in _context.Bills
                            on home.BillsId | water.BillsId | electric.BillsId | gas.BillsId equals bills.Id
                            select new BillsListUserModel()
                            {
                                Id = bills.Id,
                                HomePrice = home.Price,
                                HomeBillDate = home.BillDate,
                                ElectricPrice = electric.Price,
                                ElectricBillDate = electric.BillDate,
                                WaterPrice = water.Price,
                                WaterBillDate = water.BillDate,
                                GasPrice = gas.Price,
                                GasBillDate = gas.BillDate
                            };
                if (query.Any())
                {
                    result.entityList = query.ToList();
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Listede fatura bulunmamaktadır. Tüm faturalarınızı ödediğiniz için teşekkür ederiz :)";
                }
            }
            return result;
        }
    }
}
