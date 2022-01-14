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

        public BaseModel<BillsListUserModel> GetAllAsUser(int userId)
        {
            var result = new BaseModel<BillsListUserModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                List<BillsListUserModel> model = new();
                int myHomeId = _context.Homes.FirstOrDefault(h => h.OwnerId == userId).Id;
                var myBills = _context.Bills.Where(b => b.HomeId == myHomeId).Select(e => e.Id).ToList();
                for (int i = 0; i < myBills.Count; i++)
                {
                    var dues = _context.HomeBill.FirstOrDefault(d => d.BillsId == myBills[i]);
                    var electric = _context.ElectricBill.FirstOrDefault(d => d.BillsId == myBills[i]);
                    var water = _context.WaterBill.FirstOrDefault(d => d.BillsId == myBills[i]);
                    var gas = _context.GasBill.FirstOrDefault(d => d.BillsId == myBills[i]);

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

                    model.Add(new BillsListUserModel()
                    {
                        Id = myBills[i],
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


                if (model.Any())
                {
                    result.entityList = model;
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
