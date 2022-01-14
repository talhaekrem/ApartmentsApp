using ApartmentsApp.Models;
using ApartmentsApp.Models.Bills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Services.BillServices
{
    public interface IBillService
    {
        BaseModel<BillsListAdminModel> GetAllAsAdmin();
        BaseModel<BillsListUserModel> GetAllAsUser(int userId);
    }
}
