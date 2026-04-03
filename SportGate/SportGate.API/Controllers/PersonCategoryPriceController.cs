namespace SportGate.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SportGate.API.Data;
    using SportGate.API.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class PersonCategoryPriceController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PersonCategoryPriceController(AppDbContext db)
        {
            _db = db;
        }

        // ---------------------------------------
        // Obtener todos
        // ---------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.PersonCategoryPrices
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
            var item = await _db.PersonCategoryPrices.FindAsync(id);

            if (item == null)
                return NotFound("EntryTypePrice not found.");

            return Ok(item);
        }

        // ---------------------------------------
        // Crear
        // ---------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(PersonCategoryPrice req)
        {
            // Validar código único
            if (await _db.PersonCategoryPrices.AnyAsync(x => x.Code == req.Code))
                return BadRequest("Code must be unique.");

            var entity = new PersonCategoryPrice
            {
                Code = req.Code,
                Description = req.Description,
                Price = req.Price,
            };

            _db.PersonCategoryPrices.Add(entity);
            await _db.SaveChangesAsync();

            return Ok(entity);
        }

        // ---------------------------------------
        // Actualizar
        // ---------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonCategoryPrice req)
        {
            var entity = await _db.PersonCategoryPrices.FindAsync(id);

            if (entity == null)
                return NotFound("PersonCategoryPrice not found.");

            // Validar código único en otros registros
            if (await _db.PersonCategoryPrices.AnyAsync(x => x.Code == req.Code && x.Id != id))
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
            var entity = await _db.PersonCategoryPrices.FindAsync(id);

            if (entity == null)
                return NotFound("PersonCategoryPrice not found.");

            _db.PersonCategoryPrices.Remove(entity);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Deleted successfully." });
        }
    }
}