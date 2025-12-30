using System.ComponentModel.DataAnnotations;

namespace SportGate.API.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } // En producción debería estar hasheado

        [Required]
        public string Role { get; set; } // Administrador, Usuario, Reportes

        public bool IsActive { get; set; } = true; // Para eliminación lógica

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}

