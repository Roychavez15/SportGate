namespace SportGate.API.Data
{
    using Microsoft.EntityFrameworkCore;
    using SportGate.API.Models;

    public static class DataSeeder
    {
        public static void Seed(AppDbContext db)
        {
            // Verifica si ya hay datos
            if (!db.EntryTypePrices.Any())
            {
                db.EntryTypePrices.AddRange(
                    new EntryTypePrice
                    {
                        Code = "PEATONAL",
                        Description = "Entrada Peatonal",
                        BaseFee = 0.50m,
                        AllowMultiplePeople = false
                    },
                    new EntryTypePrice
                    {
                        Code = "AUTO",
                        Description = "Ingreso de Auto",
                        BaseFee = 1.00m,
                        AllowMultiplePeople = true
                    },
                    new EntryTypePrice
                    {
                        Code = "MOTO",
                        Description = "Ingreso de Moto",
                        BaseFee = 0.50m,
                        AllowMultiplePeople = true
                    },
                    new EntryTypePrice
                    {
                        Code = "VIP",
                        Description = "Invitado Especial / VIP",
                        BaseFee = 0.00m,
                        AllowMultiplePeople = false
                    }
                );
            }
            if (!db.PersonCategoryPrices.Any())
            {
                db.PersonCategoryPrices.AddRange(
                    new PersonCategoryPrice { Code = "ADULTO", Description = "Adulto", Price = 0.50m },
                    new PersonCategoryPrice { Code = "NIÑO", Description = "Niño", Price = 0.25m },
                    new PersonCategoryPrice { Code = "TERCERA_EDAD", Description = "Tercera edad", Price = 0.25m }
                );
            }
            db.SaveChanges();
        }
    }
}