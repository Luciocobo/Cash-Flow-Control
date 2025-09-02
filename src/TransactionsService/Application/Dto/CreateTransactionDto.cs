namespace TransactionsService.Application.Dto;

public class CreateTransactionDto
{
    public DateTime OccurredAt { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = "Credito"; // "Credito" or "Debito"
    public string? Description { get; set; }
}