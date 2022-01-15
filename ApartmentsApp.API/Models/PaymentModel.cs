using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsApp.API.Models
{
    public class PaymentModel
    {
        public string cardId { get; set; }
        public decimal total { get; set; }
    }
}
