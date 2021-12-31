using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.Models
{
    public class BaseModel<T>
    {
        public bool isSuccess { get; set; }
        public T entity { get; set; }
        public List<T> entityList { get; set; }
        public string exeptionMessage { get; set; }
    }
}
