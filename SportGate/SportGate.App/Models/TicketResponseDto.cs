using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Models
{
    public class TicketResponseDto
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public decimal TotalAmount { get; set; }
        public int PeopleCount { get; set; }
        public EntryTypePrice entryTypePrice { get; set; } = new EntryTypePrice();
        public DateTime CreatedAt { get; set; }
        public string EntryType { get; set; }
    }
}