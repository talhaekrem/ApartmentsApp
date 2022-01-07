using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Core.Bills
{
    public enum BillType
    {
        [Display(Name = "Aidat")]
        Home = 0,
        [Display(Name = "Elektrik")]
        Electric = 1,
        [Display(Name = "Su")]
        Water = 2,
        [Display(Name = "Doğalgaz")]
        Gas = 3
    }
}
