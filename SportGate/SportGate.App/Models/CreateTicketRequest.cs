using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Models
{
    public class CreateTicketRequest
    {
        public string EntryTypeCode { get; set; } = string.Empty;
        public List<CreateTicketPersonDto> People { get; set; } = new();
        public bool IncludeQrAsBase64 { get; set; } = true;
    }
}