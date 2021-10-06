using BusinessLogic.Interface;
using BusinessLogic.Service;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PromotionEngine
{
    public static class DependencyInjection
    {
        public static ServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
            .AddTransient<ICalculateService, CalculateService>()
            .AddTransient<ICalculationDiscountService, CalculationDiscountService>()
            .AddTransient<IFacadeService, FacadeService>()
            .AddDbContext<InMemoryDbContext>(options => options.UseInMemoryDatabase("TestDatabase"))
            .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
