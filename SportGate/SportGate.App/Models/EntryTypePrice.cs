using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Models
{
    public class EntryTypePrice
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; // "PEATONAL", "AUTO"
        public string Description { get; set; } = string.Empty;
        public decimal BaseFee { get; set; }
        public bool RequiresBaseFee { get; set; }
        public bool AllowMultiplePeople { get; set; }
    }
}