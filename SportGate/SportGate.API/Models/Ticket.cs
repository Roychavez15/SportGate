using System.ComponentModel.DataAnnotations;

namespace SportGate.API.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string ShortCode { get; set; } // 6–10 chars

        public int EntryTypePriceId { get; set; }
        public EntryTypePrice EntryTypePrice { get; set; }

        [Required]
        public int PeopleCount { get; set; } // 1 for peatonal, N for autos

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public List<TicketUsage> Usages { get; set; }
    }
}