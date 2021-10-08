using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.Interface;
using Microsoft.Extensions.Hosting;
using DataAccess;
using BusinessLogic.Service;
using Microsoft.EntityFrameworkCore;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().RunSeeds().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<ICalculatorTypeService, CalculatorTypeService>();
                    services.AddTransient<ICalculateService, CalculateService>();
                    services.AddTransient<IFacadeService, FacadeService>();
                    services.AddDbContext<InMemoryDbContext>(options => options.UseInMemoryDatabase("TestDatabase"));
                    services.AddHostedService<PromotionEngineInit>();
                })
                .UseConsoleLifetime(o => o.SuppressStatusMessages = true);

            return hostBuilder;
        }
    }
}
