using Microsoft.AspNetCore.Mvc;

namespace DailySummaryService.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("/health")]
        public IActionResult Get() => Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }
}
