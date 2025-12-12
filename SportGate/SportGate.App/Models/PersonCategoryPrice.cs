using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Models
{
    public class PersonCategoryPrice
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; // ADULTO, NINO, TERCERA_EDAD
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}