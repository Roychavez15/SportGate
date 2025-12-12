namespace SportGate.API.Models
{
    public class PersonCategoryPrice
    {
        public int Id { get; set; }
        public string Code { get; set; }  // ADULTO, NIÑO, TERCERA_EDAD
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}