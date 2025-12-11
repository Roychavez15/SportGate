namespace SportGate.API.Controllers
{
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
        public async Task<IActionResult> CreateTicket(CreateTicketRequest req)
        {
            var price = await _db.EntryTypePrices.FindAsync(req.EntryTypePriceId);
            if (price == null)
                return BadRequest("Invalid EntryTypePriceId");

            // generar codigo corto único
            string code;
            do
            {
                code = ShortCodeGenerator.Generate(6);
            }
            while (await _db.Tickets.AnyAsync(x => x.ShortCode == code));

            var total = price.Price * req.PeopleCount;

            var ticket = new Ticket
            {
                ShortCode = code,
                EntryTypePriceId = price.Id,
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
                EntryType = price.Description
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