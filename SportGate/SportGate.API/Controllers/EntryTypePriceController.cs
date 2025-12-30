namespace SportGate.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SportGate.API.Data;
    using SportGate.API.Models;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdministradorOnly")]
    public class EntryTypePriceController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EntryTypePriceController(AppDbContext db)
        {
            _db = db;
        }

        // ---------------------------------------
        // Obtener todos
        // ---------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.EntryTypePrices
                .OrderBy(x => x.Description)
                .ToListAsync();

            return Ok(list);
        }

        // ---------------------------------------
        // Obtener por ID
        // ---------------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _db.EntryTypePrices.FindAsync(id);

            if (item == null)
                return NotFound("EntryTypePrice not found.");

            return Ok(item);
        }

        // ---------------------------------------
        // Crear
        // ---------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(EntryTypePrice req)
        {
            // Validar código único
            if (await _db.EntryTypePrices.AnyAsync(x => x.Code == req.Code))
                return BadRequest("Code must be unique.");

            var entity = new EntryTypePrice
            {
                Code = req.Code,
                Description = req.Description,
                Price = req.Price
            };

            _db.EntryTypePrices.Add(entity);
            await _db.SaveChangesAsync();

            return Ok(entity);
        }

        // ---------------------------------------
        // Actualizar
        // ---------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EntryTypePrice req)
        {
            var entity = await _db.EntryTypePrices.FindAsync(id);

            if (entity == null)
                return NotFound("EntryTypePrice not found.");

            // Validar código único en otros registros
            if (await _db.EntryTypePrices.AnyAsync(x => x.Code == req.Code && x.Id != id))
                return BadRequest("Code must be unique.");

            entity.Code = req.Code;
            entity.Description = req.Description;
            entity.Price = req.Price;

            await _db.SaveChangesAsync();

            return Ok(entity);
        }

        // ---------------------------------------
        // Eliminar
        // ---------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.EntryTypePrices.FindAsync(id);

            if (entity == null)
                return NotFound("EntryTypePrice not found.");

            _db.EntryTypePrices.Remove(entity);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Deleted successfully." });
        }
    }
}