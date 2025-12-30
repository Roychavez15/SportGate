using System.ComponentModel.DataAnnotations;

namespace SportGate.API.DTOS
{
    public class UpdateUserRequest
    {
        [Required]
        public string Username { get; set; }

        public string? Password { get; set; } // Opcional, solo si se quiere cambiar

        [Required]
        public string Role { get; set; } // Administrador, Usuario, Reportes
    }
}

