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

        public BaseModel<BillsDetailModel> AddBill(BillsAddModel bill, BillType type)
        {
            var result = new BaseModel<BillsDetailModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                //bu daireye ait son faturanın idsini al. pkdır kendini tekrar etmez
                int lastBillId = _context.Bills.Where(b => b.HomeId == bill.HomeId).LastOrDefault().Id;

                //bu kısım daha önceden bir fatura eklenmemiş ve ilk defa fatura ekliyor demek.
                //eğer son faturanın üzerinden 30 gün geçmiş mi geçmemiş mi kontrolünü de yapıyoruz.
                //ilk önce ana bill tablosunda satır oluştururuz.
                if (bill.BillsId == null)
                {
                    if (CanTakeBill(lastBillId, type))
                    {
                        var newBill = new ApartmentsApp.DB.Entities.Bills();
                        newBill.HomeId = bill.HomeId;
                        var addedMainBill = _context.Bills.Add(newBill);
                        _context.SaveChanges();
                        bill.BillsId = addedMainBill.Entity.Id;
                    }
                    else
                    {
                        result.exeptionMessage = "Son faturanın üzerinden 1 ay geçmediği için yeni fatura ekleyemezsiniz";
                    }

                }
                else if(CanTakeBill(lastBillId,type))
                {
                    var model = (dynamic)null;
                    switch (type)
                    {
                        case BillType.Home:
                            model = _mapper.Map<ApartmentsApp.DB.Entities.HomeBill>(bill);
                            _context.HomeBill.Add(model);
                            break;
                        case BillType.Electric:
                            model = _mapper.Map<ApartmentsApp.DB.Entities.ElectricBill>(bill);
                            _context.ElectricBill.Add(model);
                            break;
                        case BillType.Water:
                            model = _mapper.Map<ApartmentsApp.DB.Entities.WaterBill>(bill);
                            _context.WaterBill.Add(model);
                            break;
                        case BillType.Gas:
                            model = _mapper.Map<ApartmentsApp.DB.Entities.GasBill>(bill);
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

        public BaseModel<BillsDetailModel> UpdateBill(BillsUpdateModel bill, BillType type)
        {
            //updatei yap 
            var result = new BaseModel<BillsDetailModel>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var model = (dynamic)null;
                switch (type)
                {
                    case BillType.Home:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.HomeBill>(bill);
                        _context.HomeBill.Add(model);
                        break;
                    case BillType.Electric:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.ElectricBill>(bill);
                        _context.ElectricBill.Add(model);
                        break;
                    case BillType.Water:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.WaterBill>(bill);
                        _context.WaterBill.Add(model);
                        break;
                    case BillType.Gas:
                        model = _mapper.Map<ApartmentsApp.DB.Entities.GasBill>(bill);
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
            if (!result.isSuccess)
            {
                result.exeptionMessage = "Fatura eklenirken hata oluştu.";

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