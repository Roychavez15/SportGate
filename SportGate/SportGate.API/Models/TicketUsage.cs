namespace SportGate.API.Models
{
    public class TicketUsage
    {
        public int Id { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public DateTime UsedAt { get; set; }
    }
}