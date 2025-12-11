namespace SportGate.API.Data
{
    using Microsoft.EntityFrameworkCore;
    using SportGate.API.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EntryTypePrice> EntryTypePrices { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketUsage> TicketUsages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasIndex(x => x.ShortCode)
                .IsUnique();
        }
    }
}