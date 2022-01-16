using NBomber.CSharp;
using NBomber.Http.CSharp;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Pacco.Services.Availability.Tests.Performance
{
    public class PerformanceTests
    {
        [Fact]
        public void get_resources()
        {
            const string baseUrl = "http://localhost:5001";
            const string stepName = "init";
            const int durationInSec = 3;
            const int expectedRps = 100;
            var endpoint = $"{baseUrl}/resources";

            var step = HttpStep.Create(stepName, ctx =>
                Task.FromResult(Http.CreateRequest("GET", endpoint)
                    .WithCheck(response => Task.FromResult(response.IsSuccessStatusCode))));

            var assertions = new[]
            {
                Assertion.ForStep(stepName, s => s.RPS > expectedRps),
                Assertion.ForStep(stepName, s => s.OkCount > durationInSec * expectedRps)
            };

            var scenario = ScenarioBuilder.CreateScenario("GET resources", new[] { step })
                .WithConcurrentCopies(1)
                .WithOutWarmUp()
                .WithDuration(TimeSpan.FromSeconds(durationInSec))
                .WithAssertions(assertions);

            NBomberRunner.RegisterScenarios(scenario)
                .Run();
        }
    }
}
