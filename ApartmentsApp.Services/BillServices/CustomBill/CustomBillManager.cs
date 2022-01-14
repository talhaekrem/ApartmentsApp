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

        #region herkese ekle
        public BaseModel<bool> AddEveryone(BillsAddMultipleModel model)
        {
            var result = new BaseModel<bool> { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                //fatura kesilebilecek evlerin idsini ve aidat bilgisini alıyorum.ana bills tablosundaki homeId aşağıdaki id seçeneği olacak
                //aidat bilgisi ise aidat tablosuna otomatik eklencek. fatura kesim tarihi ise bugün olacaktır.
                var billableHomes = _context.Homes.Where(h => h.IsOwned && h.IsActive).Select(b => new { b.Id, b.DuesPrice }).ToList();

                for (int i = 0; i < billableHomes.Count; i++)
                {
                    int homeId = billableHomes[i].Id;
                    var mainBillTable = new ApartmentsApp.DB.Entities.Bills();

                    mainBillTable.HomeId = homeId;
                    _context.Bills.Add(mainBillTable);
                    _context.SaveChanges();

                    if (model.Dues)
                    {
                        var duesTable = new ApartmentsApp.DB.Entities.HomeBill();
                        duesTable.BillsId = mainBillTable.Id;
                        duesTable.BillDate = DateTime.Now;
                        duesTable.Price = billableHomes[i].DuesPrice;
                        _context.HomeBill.Add(duesTable);
                    }
                    if (model.Electric)
                    {
                        var electricTable = new ApartmentsApp.DB.Entities.ElectricBill();
                        electricTable.BillsId = mainBillTable.Id;
                        electricTable.BillDate = model.ElectricBillDate;
                        electricTable.Price = model.ElectricPrice;
                        _context.ElectricBill.Add(electricTable);
                    }

                    if (model.Water)
                    {
                        var waterTable = new ApartmentsApp.DB.Entities.WaterBill();
                        waterTable.BillsId = mainBillTable.Id;
                        waterTable.BillDate = model.WaterBillDate;
                        waterTable.Price = model.WaterPrice;
                        _context.WaterBill.Add(waterTable);
                    }

                    if (model.Gas)
                    {
                        var gasTable = new ApartmentsApp.DB.Entities.GasBill();
                        gasTable.BillsId = mainBillTable.Id;
                        gasTable.BillDate = model.GasBillDate;
                        gasTable.Price = model.GasPrice;
                        _context.GasBill.Add(gasTable);
                    }
                    _context.SaveChanges();
                }
                result.isSuccess = true;
            };
            return result;
        }
        #endregion

        #region fatura ekle
        public BaseModel<bool> AddBill(BillsAddMultipleModel addBill)
        {
            var result = new BaseModel<bool>() { isSuccess = false };
            using (var _context = new ApartmentsAppContext())
            {
                var currentHome = _context.Homes.FirstOrDefault(h => h.Id == addBill.HomeId);

                var mainBillTable = new ApartmentsApp.DB.Entities.Bills();
                mainBillTable.HomeId = currentHome.Id;
                _context.Bills.Add(mainBillTable);
                _context.SaveChanges();
                if (addBill.Dues)
                {
                    var duesTable = new ApartmentsApp.DB.Entities.HomeBill();
                    duesTable.BillsId = mainBillTable.Id;
                    duesTable.BillDate = DateTime.Now;
                    duesTable.Price = currentHome.DuesPrice;
                    _context.HomeBill.Add(duesTable);
                }
                if (addBill.Electric)
                {
                    var electricTable = new ApartmentsApp.DB.Entities.ElectricBill();
                    electricTable.BillsId = mainBillTable.Id;
                    electricTable.BillDate = addBill.ElectricBillDate;
                    electricTable.Price = addBill.ElectricPrice;
                    _context.ElectricBill.Add(electricTable);
                }

                if (addBill.Water)
                {
                    var waterTable = new ApartmentsApp.DB.Entities.WaterBill();
                    waterTable.BillsId = mainBillTable.Id;
                    waterTable.BillDate = addBill.WaterBillDate;
                    waterTable.Price = addBill.WaterPrice;
                    _context.WaterBill.Add(waterTable);
                }

                if (addBill.Gas)
                {
                    var gasTable = new ApartmentsApp.DB.Entities.GasBill();
                    gasTable.BillsId = mainBillTable.Id;
                    gasTable.BillDate = addBill.GasBillDate;
                    gasTable.Price = addBill.GasPrice;
                    _context.GasBill.Add(gasTable);
                }
                _context.SaveChanges();
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