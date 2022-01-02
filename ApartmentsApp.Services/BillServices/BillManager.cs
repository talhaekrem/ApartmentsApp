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
        private readonly IMapper _mapper;
        public BillManager(IMapper mapper)
        {
            _mapper = mapper;
        }
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
                var query = from home in _context.HomeBill
                            from water in _context.WaterBill
                            from electric in _context.ElectricBill
                            from gas in _context.GasBill
                            join bills in _context.Bills
                            on home.BillsId | water.BillsId | electric.BillsId | gas.BillsId equals bills.Id
                            select new
                            {
                                Id = bills.Id,
                                HomeId = bills.HomeId,
                                isHomeBillPaid = home.IsPaid,
                                IsElectricBillPaid = electric.IsPaid,
                                IsWaterBillPaid = water.IsPaid,
                                IsGasBillPaid = gas.IsPaid
                            };
                if (query.Any())
                {
                    result.entityList = _mapper.Map<List<BillsListAdminModel>>(query);
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
                            select new
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
                    result.entityList = _mapper.Map<List<BillsListUserModel>>(query);
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
