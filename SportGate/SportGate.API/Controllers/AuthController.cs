using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportGate.API.Data;
using SportGate.API.DTOS;
using SportGate.API.Services;

namespace SportGate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IAuthService _authService;

        public AuthController(AppDbContext db, IAuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        // ---------------------------------------
        // Login
        // ---------------------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Username == req.Username && x.IsActive);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Validar password (en producción debería estar hasheado)
            if (user.Password != req.Password)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _authService.GenerateToken(user);

            return Ok(new LoginResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role,
                UserId = user.Id
            });
        }
    }
}

