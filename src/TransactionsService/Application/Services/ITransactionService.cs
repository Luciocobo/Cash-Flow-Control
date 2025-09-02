using TransactionsService.Domain;
using TransactionsService.Application.Dto;

namespace TransactionsService.Application.Services;

public interface ITransactionService
{
    Task<Guid> CreateAsync(CreateTransactionDto dto, CancellationToken cancellationToken = default);
    Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken = default);
}