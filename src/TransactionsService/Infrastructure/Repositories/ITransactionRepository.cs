using TransactionsService.Domain;

namespace TransactionsService.Infrastructure.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction t, CancellationToken cancellationToken = default);
    Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetByPeriodAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetAll(CancellationToken cancellationToken = default);
}