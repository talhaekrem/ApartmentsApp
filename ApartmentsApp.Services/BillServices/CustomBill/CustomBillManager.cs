using ApartmentsApp.Core.Bills;
using ApartmentsApp.DB.Entities;
using ApartmentsApp.DB.Entities.ApartmentsAppDbContext;
using ApartmentsApp.Models;
using ApartmentsApp.Models.Bills.CustomBills;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Services.BillServices.CustomBill
{
    public class CustomBillManager : ICustomBillService
    {
        private readonly IMapper _mapper;
        public CustomBillManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region fatura ekle
        public BaseModel<BillsDetailsModel> AddBill(BillsAddModel addBill, BillType type)
        {
            var result = new BaseModel<BillsDetailsModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                //bu daireye ait son faturanın idsini al. pkdır kendini tekrar etmez
                int lastBillId = 0;
                var lastBill = _context.Bills.OrderBy(s => s.Id).LastOrDefault(b => b.HomeId == addBill.HomeId);
                if (lastBill != null)
                {
                    lastBillId = lastBill.Id;
                }
                //bu kısım daha önceden bir fatura eklenmemiş ve ilk defa fatura ekliyor demek.
                //son faturanın üzerinden 30 gün geçmiş mi geçmemiş mi kontrolünü de yapıyoruz.
                //ilk önce ana bill tablosunda satır oluşturuyoruz.

                if (CanTakeBill(lastBillId, type))
                {
                    var newBill = new ApartmentsApp.DB.Entities.Bills();
                    newBill.HomeId = addBill.HomeId;
                    _context.Bills.Add(newBill);
                    _context.SaveChanges();
                    addBill.BillsId = newBill.Id;

                    switch (type)
                    {
                        case BillType.Home:
                            var home = _mapper.Map<ApartmentsApp.DB.Entities.HomeBill>(addBill);
                            _context.HomeBill.Add(home);
                            _context.SaveChanges();
                            result.isSuccess = true;
                            result.entity = _mapper.Map<BillsDetailsModel>(home);
                            break;
                        case BillType.Electric:
                            var electric = _mapper.Map<ApartmentsApp.DB.Entities.ElectricBill>(addBill);
                            _context.ElectricBill.Add(electric);
                            _context.SaveChanges();
                            result.isSuccess = true;
                            result.entity = _mapper.Map<BillsDetailsModel>(electric);
                            break;
                        case BillType.Water:
                            var water = _mapper.Map<ApartmentsApp.DB.Entities.WaterBill>(addBill);
                            _context.WaterBill.Add(water);
                            _context.SaveChanges();
                            result.isSuccess = true;
                            result.entity = _mapper.Map<BillsDetailsModel>(water);
                            break;
                        case BillType.Gas:
                            var gas = _mapper.Map<ApartmentsApp.DB.Entities.GasBill>(addBill);
                            _context.GasBill.Add(gas);
                            _context.SaveChanges();
                            result.isSuccess = true;
                            result.entity = _mapper.Map<BillsDetailsModel>(gas);
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    result.exeptionMessage = $"Son {BillHelpers.GetEnumDisplayName(type)} faturasının üzerinden 1 ay geçmediği için yeni fatura ekleyemezsiniz.";
                }
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Fatura eklenirken hata oluştu.";

            }
            return result;
        }
        #endregion

        #region tek tipte fatura ekle
        public BaseModel<BillsDetailsModel> InsertOneBill(BillsAddModel addBill, BillType type, int MainBillId)
        {
            var result = new BaseModel<BillsDetailsModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                //bu daireye ait son faturanın idsini al. pkdır kendini tekrar etmez
                int lastBillId = 0;
                var lastBill = _context.Bills.OrderBy(s => s.Id).LastOrDefault(b => b.HomeId == addBill.HomeId);
                if (lastBill != null)
                {
                    lastBillId = lastBill.Id;
                }
                //bu kısım daha önceden bir fatura eklenmemiş ve ilk defa fatura ekliyor demek.
                //son faturanın üzerinden 30 gün geçmiş mi geçmemiş mi kontrolünü de yapıyoruz.
                //ilk önce ana bill tablosunda satır oluşturuyoruz.

                if (CanTakeBill(lastBillId, type))
                {
                    switch (type)
                    {
                        case BillType.Home:
                            var home = _mapper.Map<ApartmentsApp.DB.Entities.HomeBill>(addBill);
                            home.BillsId = MainBillId;
                            _context.HomeBill.Add(home);
                            _context.SaveChanges();
                            result.isSuccess = true;
                            result.entity = _mapper.Map<BillsDetailsModel>(home);
                            break;
                        case BillType.Electric:
                            var electric = _mapper.Map<ApartmentsApp.DB.Entities.ElectricBill>(addBill);
                            electric.BillsId = MainBillId;
                            _context.ElectricBill.Add(electric);
                            _context.SaveChanges();
                            result.isSuccess = true;
                            result.entity = _mapper.Map<BillsDetailsModel>(electric);
                            break;
                        case BillType.Water:
                            var water = _mapper.Map<ApartmentsApp.DB.Entities.WaterBill>(addBill);
                            water.BillsId = MainBillId;
                            _context.WaterBill.Add(water);
                            _context.SaveChanges();
                            result.isSuccess = true;
                            result.entity = _mapper.Map<BillsDetailsModel>(water);
                            break;
                        case BillType.Gas:
                            var gas = _mapper.Map<ApartmentsApp.DB.Entities.GasBill>(addBill);
                            gas.BillsId = MainBillId;
                            _context.GasBill.Add(gas);
                            _context.SaveChanges();
                            result.isSuccess = true;
                            result.entity = _mapper.Map<BillsDetailsModel>(gas);
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    result.exeptionMessage = $"Son {BillHelpers.GetEnumDisplayName(type)} faturasının üzerinden 1 ay geçmediği için yeni fatura ekleyemezsiniz.";
                }
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Fatura eklenirken hata oluştu.";

            }
            return result;
        }
        #endregion

        #region fatura sil
        public BaseModel<bool> DeleteBill(int id, BillType type)
        {
            var result = new BaseModel<bool>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {

                switch (type)
                {
                    case BillType.Home:
                        var dues = _context.HomeBill.FirstOrDefault(h => h.BillsId == id);
                        _context.HomeBill.Remove(dues);
                        _context.SaveChanges();
                        result.isSuccess = true;
                        break;
                    case BillType.Electric:
                        var electric = _context.ElectricBill.FirstOrDefault(e => e.BillsId == id);
                        _context.ElectricBill.Remove(electric);
                        _context.SaveChanges();
                        result.isSuccess = true;
                        break;
                    case BillType.Water:
                        var water = _context.WaterBill.FirstOrDefault(w => w.BillsId == id);
                        _context.WaterBill.Remove(water);
                        _context.SaveChanges();
                        result.isSuccess = true;
                        break;
                    case BillType.Gas:
                        var gas = _context.GasBill.FirstOrDefault(g => g.BillsId == id);
                        _context.GasBill.Remove(gas);
                        _context.SaveChanges();
                        result.isSuccess = true;
                        break;
                    default:
                        result.exeptionMessage = "Fatura silinirken hata oluştu.";
                        break;
                }

            }
            return result;
        }
        #endregion

        #region idye göre fatura getir
        public BaseModel<BillsDetailsModel> GetBillDetails(int id, BillType type)
        {
            var result = new BaseModel<BillsDetailsModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                switch (type)
                {
                    case BillType.Home:
                        var dues = _context.HomeBill.FirstOrDefault(h => h.BillsId == id);
                        result.entity = _mapper.Map<BillsDetailsModel>(dues);
                        result.isSuccess = true;
                        break;
                    case BillType.Electric:
                        var electric = _context.ElectricBill.FirstOrDefault(e => e.BillsId == id);
                        result.entity = _mapper.Map<BillsDetailsModel>(electric);
                        result.isSuccess = true;
                        break;
                    case BillType.Water:
                        var water = _context.WaterBill.FirstOrDefault(w => w.BillsId == id);
                        result.entity = _mapper.Map<BillsDetailsModel>(water);
                        result.isSuccess = true;
                        break;
                    case BillType.Gas:
                        var gas = _context.GasBill.FirstOrDefault(g => g.BillsId == id);
                        result.entity = _mapper.Map<BillsDetailsModel>(gas);
                        result.isSuccess = true;
                        break;
                    default:
                        result.exeptionMessage = "Fatura tipi hatalıdır";
                        break;
                }


            }
            return result;
        }
        #endregion

        #region fatura güncelle
        public BaseModel<BillsDetailsModel> UpdateBill(BillsUpdateModel updateBill, BillType type)
        {
            var result = new BaseModel<BillsDetailsModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                switch (type)
                {
                    case BillType.Home:
                        var duesModel = _mapper.Map<ApartmentsApp.DB.Entities.HomeBill>(updateBill);
                        var home = _context.HomeBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        duesModel.PaymentDate = home.BillDate;
                        _context.Entry(home).CurrentValues.SetValues(duesModel);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailsModel>(home);
                        break;
                    case BillType.Electric:
                        var electricModel = _mapper.Map<ApartmentsApp.DB.Entities.ElectricBill>(updateBill);
                        var electric = _context.ElectricBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        electricModel.PaymentDate = electric.BillDate;
                        _context.Entry(electric).CurrentValues.SetValues(electricModel);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailsModel>(electric);
                        break;
                    case BillType.Water:
                        var waterModel = _mapper.Map<ApartmentsApp.DB.Entities.WaterBill>(updateBill);
                        var water = _context.WaterBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        waterModel.BillDate = water.BillDate;
                        _context.Entry(water).CurrentValues.SetValues(waterModel);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailsModel>(water);
                        break;
                    case BillType.Gas:
                        var gasModel = _mapper.Map<ApartmentsApp.DB.Entities.GasBill>(updateBill);
                        var gas = _context.GasBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        gasModel.BillDate = gas.BillDate;
                        _context.Entry(gas).CurrentValues.SetValues(gasModel);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailsModel>(gas);
                        break;
                    default:
                        break;
                }
                result.isSuccess = true;
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Fatura güncellenirken bir hata oluştu.";
            }
            return result;
        }
        #endregion

        #region fatura kesilebilir mi
        private bool CanTakeBill(int id, BillType type)
        {
            var isTrue = false;
            using (var _context = new ApartmentsAppContext())
            {
                switch (type)
                {
                    case BillType.Home:
                        var home = _context.HomeBill.FirstOrDefault(h => h.BillsId == id);
                        if (home == null || BillHelpers.IsNextMonth(home.BillDate))
                        {
                            isTrue = true;
                        }
                        break;
                    case BillType.Electric:
                        var electric = _context.ElectricBill.FirstOrDefault(e => e.BillsId == id);
                        if (electric == null || BillHelpers.IsNextMonth(electric.BillDate))
                        {
                            isTrue = true;
                        }
                        break;
                    case BillType.Water:
                        var water = _context.WaterBill.FirstOrDefault(w => w.BillsId == id);
                        if (water == null || BillHelpers.IsNextMonth(water.BillDate))
                        {
                            isTrue = true;
                        }
                        break;
                    case BillType.Gas:
                        var gas = _context.GasBill.FirstOrDefault(g => g.BillsId == id);
                        if (gas == null || BillHelpers.IsNextMonth(gas.BillDate))
                        {
                            isTrue = true;
                        }
                        break;
                    default:
                        break;
                }
            };
            return isTrue;
        }
        #endregion
    }
}