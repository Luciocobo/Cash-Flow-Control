using DailySummaryService.Application.Services;
using DailySummaryService.Domain;
using DailySummaryService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DailySummaryService.Tests
{
    public class DailyBalanceServiceTests
    {
        private AppDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetByDateAsync_ShouldReturnNull_WhenNoBalanceExists()
        {
            // Arrange
            var db = GetInMemoryDb();
            var service = new DailyBalanceService(db);

            // Act
            var result = await service.GetByDateAsync(DateTime.UtcNow);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task RecomputeAsync_ShouldCreateBalance_WhenNoneExists()
        {
            // Arrange
            var db = GetInMemoryDb();
            var service = new DailyBalanceService(db);
            var today = DateTime.UtcNow.Date;

            var transactions = new List<(decimal amount, string type)>
            {
                (100, "Credit"),
                (40, "Debit")
            };

            // Act
            var balance = await service.RecomputeAsync(today, transactions);

            // Assert
            Assert.NotNull(balance);
            Assert.Equal(100, balance.TotalCredits);
            Assert.Equal(40, balance.TotalDebits);
            Assert.Equal(60, balance.Balance);
        }

        [Fact]
        public async Task RecomputeAsync_ShouldUpdateExistingBalance()
        {
            // Arrange
            var db = GetInMemoryDb();
            var service = new DailyBalanceService(db);
            var today = DateTime.UtcNow.Date;

            // Primeiro cálculo
            await service.RecomputeAsync(today, new List<(decimal, string)>
            {
                (200, "Credit"),
                (50, "Debit")
            });

            // Segundo cálculo (corrigindo os dados)
            var updatedBalance = await service.RecomputeAsync(today, new List<(decimal, string)>
            {
                (300, "Credit"),
                (120, "Debit")
            });

            // Assert
            Assert.Equal(300, updatedBalance.TotalCredits);
            Assert.Equal(120, updatedBalance.TotalDebits);
            Assert.Equal(180, updatedBalance.Balance);
        }
    }
}
