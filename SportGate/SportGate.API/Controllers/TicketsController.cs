namespace SportGate.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SportGate.API.Data;
    using SportGate.API.DTOS;
    using SportGate.API.Helpers;
    using SportGate.API.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TicketsController(AppDbContext db)
        {
            _db = db;
        }

        // ---------------------------------------
        // Crear Ticket
        // ---------------------------------------
        [HttpPost("create")]
        [Authorize(Policy = "AdministradorOrUsuario")]
        public async Task<IActionResult> CreateTicket(CreateTicketRequest req)
        {
            var type = await _db.EntryTypePrices.FindAsync(req.EntryTypePriceId);
            if (type == null)
                return BadRequest("Invalid EntryTypePriceId");

            if (!type.AllowMultiplePeople && req.People.Sum(p => p.Quantity) != 1)
                return BadRequest("This entry type only allows exactly 1 person");

            decimal total = 0;

            // 1. Si tiene tarifa base (Auto, Moto, etc.)
            if (type.RequiresBaseFee)
                total += type.BaseFee;

            // 2. Personas individuales con categorías
            foreach (var p in req.People)
            {
                var cat = await _db.PersonCategoryPrices
                    .FirstOrDefaultAsync(x => x.Id == p.PersonCategoryId);

                if (cat == null)
                    return BadRequest($"Category {p.PersonCategoryId} not found");

                total += cat.Price * p.Quantity;
            }

            // generar codigo corto único
            string code;
            do
            {
                code = ShortCodeGenerator.Generate(6);
            }
            while (await _db.Tickets.AnyAsync(x => x.ShortCode == code));

            //var total = price.Price * req.PeopleCount;

            var ticket = new Ticket
            {
                ShortCode = code,
                EntryTypePriceId = type.Id,
                PeopleCount = req.PeopleCount,
                TotalAmount = total,
                CreatedAt = DateTime.Now
            };

            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                ticket.Id,
                ticket.ShortCode,
                ticket.TotalAmount,
                ticket.PeopleCount,
                EntryType = type.Description
            });
        }

        // ---------------------------------------
        // VALIDAR TICKET (GET)
        // ejemplo:
        // GET /api/Tickets/validate?code=WE5CNU
        // ---------------------------------------
        [HttpGet("validate")]
        public async Task<IActionResult> ValidateTicketGet([FromQuery] string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return BadRequest("Code is required.");

            var ticket = await _db.Tickets
                .Include(x => x.EntryTypePrice)
                .FirstOrDefaultAsync(x => x.ShortCode == code);

            if (ticket == null)
                return BadRequest("Ticket not found");

            // validar por día (solo válido el día actual)
            if (ticket.CreatedAt.Date != DateTime.Now.Date)
                return BadRequest("Ticket expired (not from today)");

            // registrar uso
            var usage = new TicketUsage
            {
                TicketId = ticket.Id,
                UsedAt = DateTime.Now
            };

            _db.TicketUsages.Add(usage);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "Ticket valid",
                ticket.ShortCode,
                UsedAt = usage.UsedAt,
                EntryType = ticket.EntryTypePrice.Description,
                ticket.PeopleCount
            });
        }

        // ---------------------------------------
        // Validar Ticket
        // ---------------------------------------
        [HttpPost("validate")]
        public async Task<IActionResult> ValidateTicket(ValidateTicketRequest req)
        {
            var ticket = await _db.Tickets
                .Include(x => x.EntryTypePrice)
                .FirstOrDefaultAsync(x => x.ShortCode == req.ShortCode);

            if (ticket == null)
                return BadRequest("Ticket not found");

            // validar por fecha (solo dia actual)
            if (ticket.CreatedAt.Date != DateTime.Now.Date)
                return BadRequest("Ticket expired (not from today)");

            // registrar uso
            var usage = new TicketUsage
            {
                TicketId = ticket.Id,
                UsedAt = DateTime.Now
            };

            _db.TicketUsages.Add(usage);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "Ticket valid",
                ticket.ShortCode,
                UsedAt = usage.UsedAt,
                EntryType = ticket.EntryTypePrice.Description,
                ticket.PeopleCount
            });
        }
    }
}