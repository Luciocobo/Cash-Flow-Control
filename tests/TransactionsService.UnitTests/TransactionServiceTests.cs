using System;
using Xunit;
using Moq;
using TransactionsService.Application.Services;
using TransactionsService.Infrastructure.Repositories;
using TransactionsService.Application.Dto;
using TransactionsService.Domain;
using System.Threading.Tasks;

public class TransactionServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldReturnGuid()
    {
        var repoMock = new Mock<ITransactionRepository>();
        repoMock.Setup(r => r.AddAsync(It.IsAny<Transaction>(), default)).Returns(Task.CompletedTask);

        var service = new TransactionService(repoMock.Object);

        var dto = new CreateTransactionDto { OccurredAt = DateTime.UtcNow, Amount = 10.5m, Type = "Credito" };
        var id = await service.CreateAsync(dto);

        Assert.NotEqual(Guid.Empty, id);
        repoMock.Verify(r => r.AddAsync(It.IsAny<Transaction>(), default), Times.Once);
    }
}