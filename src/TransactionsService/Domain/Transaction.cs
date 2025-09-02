using System.ComponentModel.DataAnnotations;

namespace TransactionsService.Domain;

public enum TransactionType { Credito = 1, Debito = 2 }

public class Transaction
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime OccurredAt { get; set; } // UTC
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string? Description { get; set; }
}