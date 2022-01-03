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

        public BaseModel<BillsDetailModel> AddBill(BillsAddModel addBill, BillType type)
        {
            var result = new BaseModel<BillsDetailModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                //bu daireye ait son faturanın idsini al. pkdır kendini tekrar etmez
                int lastBillId = _context.Bills.Where(b => b.HomeId == addBill.HomeId).LastOrDefault().Id;

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
                        result.exeptionMessage = "Son faturanın üzerinden 1 ay geçmediği için yeni fatura ekleyemezsiniz";
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
                    result.entity = _mapper.Map<BillsDetailModel>(model);
                }
                else
                {
                    result.exeptionMessage = "Son faturanın üzerinden 1 ay geçmediği için yeni fatura ekleyemezsiniz";
                }
            }
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Fatura eklenirken hata oluştu.";

            }
            return result;
        }

        public BaseModel<bool> DeleteBill(int id, BillType type)
        {
            var result = new BaseModel<bool>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var model = (dynamic)null;
                switch (type)
                {
                    case BillType.Home:
                        model = _context.HomeBill.Where(h => h.Id == id).FirstOrDefault();
                        _context.HomeBill.Remove(model);
                        break;
                    case BillType.Electric:
                        model = _context.ElectricBill.Where(e => e.Id == id).FirstOrDefault();
                        _context.ElectricBill.Remove(model);
                        break;
                    case BillType.Water:
                        model = _context.WaterBill.Where(w => w.Id == id).FirstOrDefault();
                        _context.WaterBill.Remove(model);
                        break;
                    case BillType.Gas:
                        model = _context.GasBill.Where(g => g.Id == id).FirstOrDefault();
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

        public BaseModel<BillsDetailModel> GetBillDetails(int id, BillType type)
        {
            var result = new BaseModel<BillsDetailModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var query = (dynamic)null;
                //veya
                //dynamic query = null;
                switch (type)
                {
                    case BillType.Home:
                        query = _context.HomeBill.Where(h => h.Id == id).FirstOrDefault();
                        break;
                    case BillType.Electric:
                        query = _context.ElectricBill.Where(h => h.Id == id).FirstOrDefault();
                        break;
                    case BillType.Water:
                        query = _context.WaterBill.Where(h => h.Id == id).FirstOrDefault();
                        break;
                    case BillType.Gas:
                        query = _context.GasBill.Where(h => h.Id == id).FirstOrDefault();
                        break;
                    default:
                        query = null;
                        break;
                }

                if (query is not null)
                {
                    result.entity = _mapper.Map<BillsDetailModel>(query);
                    result.isSuccess = true;
                }
                else
                {
                    result.exeptionMessage = "Fatura tipi hatalıdır";
                }
            }
            return result;
        }

        public BaseModel<BillsDetailModel> UpdateBill(BillsUpdateModel updateBill, BillType type)
        {
            //updatei yap 
            var result = new BaseModel<BillsDetailModel>() { isSuccess = false };
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
                        result.entity = _mapper.Map<BillsDetailModel>(home);
                        break;
                    case BillType.Electric:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.ElectricBill>(updateBill);
                        var electric = _context.ElectricBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        model.PaymentDate = electric.PaymentDate;
                        _context.Entry(electric).CurrentValues.SetValues(model);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailModel>(electric);
                        break;
                    case BillType.Water:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.WaterBill>(updateBill);
                        var water = _context.WaterBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        model.PaymentDate = water.PaymentDate;
                        _context.Entry(water).CurrentValues.SetValues(model);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailModel>(water);
                        break;
                    case BillType.Gas:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.GasBill>(updateBill);
                        var gas = _context.GasBill.FirstOrDefault(h => h.Id == updateBill.Id);
                        model.PaymentDate = gas.PaymentDate;
                        _context.Entry(gas).CurrentValues.SetValues(model);
                        _context.SaveChanges();
                        result.entity = _mapper.Map<BillsDetailModel>(gas);
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

        private bool CanTakeBill(int id, BillType type)
        {
            var isTrue = false;
            using (var _context = new ApartmentsAppContext())
            {
                switch (type)
                {
                    case BillType.Home:
                        if (BillHelpers.IsNextMonth(_context.HomeBill.Where(h => h.BillsId == id).FirstOrDefault().BillDate))
                        {
                            isTrue = true;
                        }
                        break;
                    case BillType.Electric:
                        if (BillHelpers.IsNextMonth(_context.ElectricBill.Where(h => h.BillsId == id).FirstOrDefault().BillDate))
                        {
                            isTrue = true;
                        }
                        break;
                    case BillType.Water:
                        if (BillHelpers.IsNextMonth(_context.WaterBill.Where(h => h.BillsId == id).FirstOrDefault().BillDate))
                        {
                            isTrue = true;
                        }
                        break;
                    case BillType.Gas:
                        if (BillHelpers.IsNextMonth(_context.GasBill.Where(h => h.BillsId == id).FirstOrDefault().BillDate))
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
    }
}