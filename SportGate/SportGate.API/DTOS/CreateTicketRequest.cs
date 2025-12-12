namespace SportGate.API.DTOS
{
    public class CreateTicketRequest
    {
        public int EntryTypePriceId { get; set; }
        public int PeopleCount { get; set; }
        public List<PersonCategoryRequest> People { get; set; }
    }
}