using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Models
{
    public class CreateTicketRequest
    {
        public int entryTypePriceId { get; set; }
        public List<CreateTicketPersonDto> People { get; set; } = new();
        public int PeopleCount { get; set; }
        //public bool IncludeQrAsBase64 { get; set; } = true;
    }
}