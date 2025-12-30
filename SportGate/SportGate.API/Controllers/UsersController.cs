namespace SportGate.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SportGate.API.Data;
    using SportGate.API.DTOS;
    using SportGate.API.Models;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdministradorOnly")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        // ---------------------------------------
        // Obtener todos los usuarios activos
        // ---------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
        {
            var query = _db.Users.AsQueryable();

            if (!includeInactive)
            {
                query = query.Where(x => x.IsActive);
            }

            var list = await query
                .OrderBy(x => x.Username)
                .Select(x => new
                {
                    x.Id,
                    x.Username,
                    x.Role,
                    x.IsActive,
                    x.CreatedAt,
                    x.UpdatedAt
                })
                .ToListAsync();

            return Ok(list);
        }

        // ---------------------------------------
        // Obtener usuario por ID
        // ---------------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _db.Users
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.Username,
                    x.Role,
                    x.IsActive,
                    x.CreatedAt,
                    x.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        // ---------------------------------------
        // Crear usuario
        // ---------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest req)
        {
            // Validar que el rol sea válido
            var validRoles = new[] { "Administrador", "Usuario", "Reportes" };
            if (!validRoles.Contains(req.Role))
            {
                return BadRequest($"Invalid role. Valid roles are: {string.Join(", ", validRoles)}");
            }

            // Validar que el username sea único
            if (await _db.Users.AnyAsync(x => x.Username == req.Username))
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = req.Username,
                Password = req.Password, // En producción debería hashearse
                Role = req.Role,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Role,
                user.IsActive,
                user.CreatedAt
            });
        }

        // ---------------------------------------
        // Actualizar usuario
        // ---------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserRequest req)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found.");

            // Validar que el rol sea válido
            var validRoles = new[] { "Administrador", "Usuario", "Reportes" };
            if (!validRoles.Contains(req.Role))
            {
                return BadRequest($"Invalid role. Valid roles are: {string.Join(", ", validRoles)}");
            }

            // Validar que el username sea único en otros registros
            if (await _db.Users.AnyAsync(x => x.Username == req.Username && x.Id != id))
            {
                return BadRequest("Username already exists.");
            }

            user.Username = req.Username;
            user.Role = req.Role;
            user.UpdatedAt = DateTime.Now;

            // Solo actualizar password si se proporciona
            if (!string.IsNullOrWhiteSpace(req.Password))
            {
                user.Password = req.Password; // En producción debería hashearse
            }

            await _db.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Role,
                user.IsActive,
                user.CreatedAt,
                user.UpdatedAt
            });
        }

        // ---------------------------------------
        // Deshabilitar usuario (eliminación lógica)
        // ---------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found.");

            // Eliminación lógica: solo deshabilitar
            user.IsActive = false;
            user.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            return Ok(new { message = "User disabled successfully." });
        }

        // ---------------------------------------
        // Habilitar usuario (reactivar)
        // ---------------------------------------
        [HttpPost("{id}/enable")]
        public async Task<IActionResult> Enable(int id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found.");

            user.IsActive = true;
            user.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            return Ok(new { message = "User enabled successfully." });
        }
    }
}

