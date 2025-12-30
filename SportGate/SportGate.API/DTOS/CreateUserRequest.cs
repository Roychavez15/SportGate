using System.ComponentModel.DataAnnotations;

namespace SportGate.API.DTOS
{
    public class CreateUserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // Administrador, Usuario, Reportes
    }
}

