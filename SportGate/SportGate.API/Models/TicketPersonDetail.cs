namespace SportGate.API.Models
{
    public class TicketPersonDetail
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int PersonCategoryId { get; set; }
        public int Quantity { get; set; }

        public Ticket Ticket { get; set; }
        public PersonCategoryPrice Category { get; set; }
    }
}