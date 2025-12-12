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

        public decimal BaseFee { get; set; } = 0;
        public bool RequiresBaseFee { get; set; } = false;

        public bool AllowMultiplePeople { get; set; } = false;

        public bool IsActive { get; set; } = true;
    }
}