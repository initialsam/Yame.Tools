using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Http.CSharp;

namespace NBomberNote
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scenario1 = 官方網站hello_world();
            var scenario2 = 官方網站Http();

            NBomberRunner
                .RegisterScenarios(scenario1, scenario2)
                .Run();
        }

        private static ScenarioProps 官方網站Http()
        {
            var httpClient = new HttpClient();

            var scenario = Scenario.Create("http_scenario", async context =>
            {
                var request =
                    Http.CreateRequest("GET", "https://nbomber.com")
                        .WithHeader("Accept", "text/html")
                        .WithBody(new StringContent("{ some JSON }"));

                var response = await Http.Send(httpClient, request);

                return response;
            })
            .WithWarmUpDuration(TimeSpan.FromSeconds(10))
            .WithLoadSimulations(
                Simulation.RampingInject(rate: 3,
                                         interval: TimeSpan.FromSeconds(1),
                                         during: TimeSpan.FromSeconds(10)),

                Simulation.Inject(rate: 4,
                                  interval: TimeSpan.FromSeconds(1),
                                  during: TimeSpan.FromSeconds(10)),
                Simulation.RampingInject(rate: 2,
                                         interval: TimeSpan.FromSeconds(1),
                                         during: TimeSpan.FromSeconds(10))
            );
            return scenario;

        }

        private static ScenarioProps 官方網站hello_world()
        {
            var scenario = Scenario.Create("hello_world_scenario", async context =>
            {
                // you can define and execute any logic here,
                // for example: send http request, SQL query etc
                // NBomber will measure how much time it takes to execute your logic
                await Task.Delay(GenerateRandomNumber(200,2000));

                return Response.Ok();
            })
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.RampingInject(rate: 60,
                                        interval: TimeSpan.FromSeconds(1),
                                        during: TimeSpan.FromSeconds(60)),
                Simulation.Inject(rate: 60,
                                    interval: TimeSpan.FromSeconds(1),
                                    during: TimeSpan.FromSeconds(30)),
                    Simulation.RampingInject(rate: 0,
                                        interval: TimeSpan.FromSeconds(1),
                                        during: TimeSpan.FromSeconds(60))
            );

            return scenario;
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            Random random = new Random();
            int randomNumber = random.Next(minValue, maxValue + 1);
            return randomNumber;
        }
    }
}