using Microsoft.EntityFrameworkCore;
using TransactionsService.Domain;
using TransactionsService.Infrastructure.Repositories;
using TransactionsService.Infrastructure.Persistence; 
using TransactionsService.Application.Dto;

namespace TransactionsService.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repo;
   
    public TransactionService(ITransactionRepository repo)
    {      
        _repo = repo; 
    }

    public async Task<Guid> CreateAsync(CreateTransactionDto dto, CancellationToken cancellationToken = default)
    {
        var t = new Transaction
        {
            OccurredAt = dto.OccurredAt.ToUniversalTime(),
            Amount = dto.Amount,
            Type = dto.Type.Equals("Credito", StringComparison.OrdinalIgnoreCase) ? TransactionType.Credito : TransactionType.Debito,
            Description = dto.Description
        };

        await _repo.AddAsync(t, cancellationToken);
        return t.Id;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repo.GetByIdAsync(id, cancellationToken); 
    }
    

    public async Task<IEnumerable<Transaction>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default)
    {      
        var targetDate = date.Date.ToUniversalTime();
        var nextDay = targetDate.AddDays(1);
        return await _repo.GetByPeriodAsync(targetDate,nextDay,cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken = default)
    {       
        return await _repo.GetAll(); 
    }
}