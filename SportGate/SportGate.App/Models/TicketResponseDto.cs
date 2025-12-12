using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Models
{
    public class TicketResponseDto
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string QrBase64Png { get; set; } = string.Empty;
        public string QrPayload { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}