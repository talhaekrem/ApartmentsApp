﻿using ApartmentsApp.Core.Bills;
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
        BaseModel<BillsDetailsModel> GetBillDetails(int id, BillType type);
        BaseModel<BillsDetailsModel> AddBill(BillsAddModel addBill, BillType type);
        BaseModel<BillsDetailsModel> InsertOneBill(BillsAddModel addBill, BillType type, int MainBillId);
        BaseModel<BillsDetailsModel> UpdateBill(BillsUpdateModel updateBill, BillType type);
        BaseModel<bool> DeleteBill(int id, BillType type);
    }
}
