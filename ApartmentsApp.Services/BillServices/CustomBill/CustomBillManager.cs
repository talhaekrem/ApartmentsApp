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
                int lastBillId = _context.Bills.LastOrDefault(b => b.HomeId == addBill.HomeId).Id;

                //bu kısım daha önceden bir fatura eklenmemiş ve ilk defa fatura ekliyor demek.
                //eğer son faturanın üzerinden 30 gün geçmiş mi geçmemiş mi kontrolünü de yapıyoruz.
                //ilk önce ana bill tablosunda satır oluştururuz.
                if (addBill.BillsId == null)
                {
                    if (CanTakeBill(lastBillId, type))
                    {
                        var newBill = new ApartmentsApp.DB.Entities.Bills();
                        newBill.HomeId = addBill.HomeId;
                        var addedMainBill = _context.Bills.Add(newBill);
                        _context.SaveChanges();
                        addBill.BillsId = addedMainBill.Entity.Id;
                    }
                    else
                    {
                        result.exeptionMessage = $"Son {BillHelpers.GetEnumDisplayName(type)} faturasının üzerinden 1 ay geçmediği için yeni fatura ekleyemezsiniz.";
                    }

                }
                else if (CanTakeBill(lastBillId, type))
                {
                    var model = (dynamic)null;
                    switch (type)
                    {
                        case BillType.Home:
                            model = _mapper.Map<ApartmentsApp.DB.Entities.HomeBill>(addBill);
                            _context.HomeBill.Add(model);
                            break;
                        case BillType.Electric:
                            model = _mapper.Map<ApartmentsApp.DB.Entities.ElectricBill>(addBill);
                            _context.ElectricBill.Add(model);
                            break;
                        case BillType.Water:
                            model = _mapper.Map<ApartmentsApp.DB.Entities.WaterBill>(addBill);
                            _context.WaterBill.Add(model);
                            break;
                        case BillType.Gas:
                            model = _mapper.Map<ApartmentsApp.DB.Entities.GasBill>(addBill);
                            _context.GasBill.Add(model);
                            break;
                        default:
                            model = null;
                            break;
                    }
                    _context.SaveChanges();
                    result.isSuccess = true;
                    result.entity = _mapper.Map<BillsDetailsModel>(model);
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
                var model = (dynamic)null;
                switch (type)
                {
                    case BillType.Home:
                        model = _context.HomeBill.FirstOrDefault(h => h.Id == id);
                        _context.HomeBill.Remove(model);
                        break;
                    case BillType.Electric:
                        model = _context.ElectricBill.FirstOrDefault(e => e.Id == id);
                        _context.ElectricBill.Remove(model);
                        break;
                    case BillType.Water:
                        model = _context.WaterBill.FirstOrDefault(w => w.Id == id);
                        _context.WaterBill.Remove(model);
                        break;
                    case BillType.Gas:
                        model = _context.GasBill.FirstOrDefault(g => g.Id == id);
                        _context.GasBill.Remove(model);
                        break;
                    default:
                        model = null;
                        break;
                }
                _context.SaveChanges();
                result.isSuccess = true;
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Fatura silinirken hata oluştu.";
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
                var query = (dynamic)null;
                //veya
                //dynamic query = null;
                switch (type)
                {
                    case BillType.Home:
                        query = _context.HomeBill.FirstOrDefault(h => h.Id == id);
                        break;
                    case BillType.Electric:
                        query = _context.ElectricBill.FirstOrDefault(e => e.Id == id);
                        break;
                    case BillType.Water:
                        query = _context.WaterBill.FirstOrDefault(w => w.Id == id);
                        break;
                    case BillType.Gas:
                        query = _context.GasBill.FirstOrDefault(g => g.Id == id);
                        break;
                    default:
                        query = null;
                        break;
                }

                if (query is not null)
                {
                    result.entity = _mapper.Map<BillsDetailsModel>(query);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Fatura tipi hatalıdır";
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
                var model = (dynamic)null;
                switch (type)
                {
                    case BillType.Home:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.HomeBill>(updateBill);
                        var home = _context.HomeBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        model.PaymentDate = home.PaymentDate;
                        _context.Entry(home).CurrentValues.SetValues(model);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailsModel>(home);
                        break;
                    case BillType.Electric:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.ElectricBill>(updateBill);
                        var electric = _context.ElectricBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        model.PaymentDate = electric.PaymentDate;
                        _context.Entry(electric).CurrentValues.SetValues(model);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailsModel>(electric);
                        break;
                    case BillType.Water:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.WaterBill>(updateBill);
                        var water = _context.WaterBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        model.PaymentDate = water.PaymentDate;
                        _context.Entry(water).CurrentValues.SetValues(model);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailsModel>(water);
                        break;
                    case BillType.Gas:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.GasBill>(updateBill);
                        var gas = _context.GasBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        model.PaymentDate = gas.PaymentDate;
                        _context.Entry(gas).CurrentValues.SetValues(model);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailsModel>(gas);
                        break;
                    default:
                        model = null;
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
                        if (BillHelpers.IsNextMonth(_context.HomeBill.FirstOrDefault(h => h.BillsId == id).BillDate))
                        {
                            isTrue = true;
                        }
                        break;
                    case BillType.Electric:
                        if (BillHelpers.IsNextMonth(_context.ElectricBill.FirstOrDefault(e => e.BillsId == id).BillDate))
                        {
                            isTrue = true;
                        }
                        break;
                    case BillType.Water:
                        if (BillHelpers.IsNextMonth(_context.WaterBill.FirstOrDefault(w => w.BillsId == id).BillDate))
                        {
                            isTrue = true;
                        }
                        break;
                    case BillType.Gas:
                        if (BillHelpers.IsNextMonth(_context.GasBill.FirstOrDefault(g => g.BillsId == id).BillDate))
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