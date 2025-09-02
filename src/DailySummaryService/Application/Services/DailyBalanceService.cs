using DailySummaryService.Domain;
using DailySummaryService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DailySummaryService.Application.Services
{
    public class DailyBalanceService
    {
        private readonly AppDbContext _db;

        public DailyBalanceService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<DailyBalance?> GetByDateAsync(DateTime date, CancellationToken ct = default)
        {
            var targetDate = date.Date.ToUniversalTime();
            return await _db.DailyBalances.FirstOrDefaultAsync(d => d.Date == targetDate, ct);
        }

        public async Task<DailyBalance> RecomputeAsync(DateTime date, IEnumerable<(decimal amount, string type)> transactions, CancellationToken ct = default)
        {
             var targetDate = date.Date.ToUniversalTime();
            var credits = transactions.Where(t => t.type == "Credito").Sum(t => t.amount);
            var debits = transactions.Where(t => t.type == "Debito").Sum(t => t.amount);

            var balance = await _db.DailyBalances.FirstOrDefaultAsync(d => d.Date == targetDate, ct);
            if (balance == null)
            {
                balance = new DailyBalance { Id = Guid.NewGuid(), Date = targetDate };
                _db.DailyBalances.Add(balance);
            }

            balance.TotalCredits = credits;
            balance.TotalDebits = debits;
            balance.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync(ct);

            return balance;
        }
    }
}
