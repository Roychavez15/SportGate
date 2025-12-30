using System.ComponentModel.DataAnnotations;

namespace SportGate.API.DTOS
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

