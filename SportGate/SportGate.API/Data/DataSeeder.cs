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
                        Price = 0.50m
                    },
                    new EntryTypePrice
                    {
                        Code = "AUTO",
                        Description = "Ingreso de Auto",
                        Price = 1.00m
                    },
                    new EntryTypePrice
                    {
                        Code = "MOTO",
                        Description = "Ingreso de Moto",
                        Price = 0.50m
                    },
                    new EntryTypePrice
                    {
                        Code = "VIP",
                        Description = "Invitado Especial / VIP",
                        Price = 0.00m
                    }
                );

                db.SaveChanges();
            }
        }
    }
}