using NBomber.Contracts;
using NBomber.CSharp;
using System.Net.Http;

class Program
{
    static void Main(string[] args)
    {
        using var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5005") };

        var scenario = Scenario.Create("get_daily_balance", async context =>
        {
            var date = DateTime.UtcNow.Date.ToString("yyyy-MM-dd");
            var response = await httpClient.GetAsync($"/api/daily/{date}");

            if (response.IsSuccessStatusCode)
            {
                return Response.Ok(
                    payload: $"StatusCode: {(int)response.StatusCode}"
                );
            }
            else
            {
                return Response.Fail(
                    payload: $"HTTP {(int)response.StatusCode}"
                );
            }
        })
        .WithLoadSimulations(
            Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30))
        );

        NBomberRunner
            .RegisterScenarios(scenario)
            .Run();
    }
}
