using DailySummaryService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DailySummaryService.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<DailyBalance> DailyBalances { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyBalance>()
                .HasIndex(d => d.Date)
                .IsUnique();
        }
    }
}
