using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Console.Tests
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                if (string.IsNullOrWhiteSpace(env))
                {
                    env = "Development";
                }

                var builder = new ConfigurationBuilder()
                                .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                                .AddEnvironmentVariables();

                IConfigurationRoot configuration = builder.Build();

                var services = new ServiceCollection();
                services
                    .AddLogging(builder => builder.AddConsole())
                    .AddInfrastructureServices(configuration)
                    .AddTransient<Tests>();

                var provider = services.BuildServiceProvider();
                var tests = provider.GetService<Tests>();
                await tests.Run();
            }
            finally
            {
                System.Console.WriteLine($"{Environment.NewLine}Press any key to exit...");
                System.Console.ReadKey();
            }
        }
    }
}
