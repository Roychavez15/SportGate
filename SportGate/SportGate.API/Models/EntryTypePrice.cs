using System.ComponentModel.DataAnnotations;

namespace SportGate.API.Models
{
    public class EntryTypePrice
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }  // PEATONAL, AUTO, etc.

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}