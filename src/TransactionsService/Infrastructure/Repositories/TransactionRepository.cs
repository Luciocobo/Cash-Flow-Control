using Microsoft.EntityFrameworkCore;
using TransactionsService.Domain;
using TransactionsService.Infrastructure.Persistence;

namespace TransactionsService.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _db;
    public TransactionRepository(AppDbContext db) { _db = db; }

    public async Task AddAsync(Transaction t, CancellationToken cancellationToken = default)
    {
        _db.Transactions.Add(t);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Transactions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IEnumerable<Transaction>> GetByPeriodAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default)
    {
        IQueryable<Transaction> query = _db.Transactions;
        query = _db.Transactions.Where(x => x.OccurredAt >= start && x.OccurredAt < end);
        return await query.ToListAsync(cancellationToken); 
    }
    
    public Task<IEnumerable<Transaction>> GetAll(CancellationToken cancellationToken = default) =>
        _db.Transactions.ToListAsync(cancellationToken).ContinueWith(t => (IEnumerable<Transaction>)t.Result);
   
}