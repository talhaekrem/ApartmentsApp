using ApartmentsApp.Core.Bills;
using ApartmentsApp.Models;
using ApartmentsApp.Models.Bills.CustomBills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Services.BillServices.CustomBill
{
    public interface ICustomBillService
    {
        BaseModel<BillsDetailModel> GetBillDetails(int id, BillType type);
        BaseModel<BillsDetailModel> AddBill(BillsAddModel bill, BillType type);
        BaseModel<BillsDetailModel> UpdateBill(BillsUpdateModel bill, BillType type);
        BaseModel<bool> DeleteBill(int id, BillType type);
    }
}
