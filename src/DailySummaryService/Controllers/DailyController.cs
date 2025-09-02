using DailySummaryService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailySummaryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyController : ControllerBase
    {
        private readonly DailyBalanceService _service;

        public DailyController(DailyBalanceService service)
        {
            _service = service;
        }

        // GET /api/daily/{date}
        [HttpGet("{date}")]
        public async Task<IActionResult> GetByDate(DateTime date, CancellationToken ct)
        {
            var balance = await _service.GetByDateAsync(date, ct);
            if (balance == null) return NotFound();
            return Ok(balance);
        }

        // POST /api/daily/{date}/recompute
        [HttpPost("{date}/recompute")]
        public async Task<IActionResult> Recompute(DateTime date, [FromBody] List<TransactionDto> transactions, CancellationToken ct)
        {
            var tx = transactions.Select(t => (t.Amount, t.Type));
            var balance = await _service.RecomputeAsync(date, tx, ct);
            return Ok(balance);
        }
    }

    public class TransactionDto
    {
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty; // "Credito" ou "Debito"
    }
}
