using Microsoft.EntityFrameworkCore;
using TrackMoney.Data.Models.Entities;

namespace TrackMoney.Data.Context
{
    public class TrackMoneyDbContext : DbContext
    {
        public TrackMoneyDbContext(DbContextOptions<TrackMoneyDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }

    }
}
