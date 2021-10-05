using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class PromotionEngineInit : IHostedService
    {
        private readonly IFacadeService _facadeService;

        public PromotionEngineInit(IFacadeService facadeService)
        {
            _facadeService = facadeService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var products = _facadeService.GetAllProducts();

            Console.WriteLine(_facadeService.DisplayPromotions());
            Console.WriteLine("Products: " + string.Join(", ", products.Select(x => x.SKU)));

            var order = new Order();
            foreach (var product in products)
            {
                bool repeat = true;
                while (repeat)
                {
                    Console.Write($"Enter amount of product {product.SKU}: ");
                    string input = Console.ReadLine();

                    int quantity;
                    if (int.TryParse(input, out quantity))
                    {
                        repeat = false;
                        order.Items.Add(new OrderItem() { SKU = product.SKU, Quantity = quantity, Price = product.Price });
                    }
                }
            }

            Console.WriteLine(_facadeService.CalculateOrder(order));
            Console.ReadLine();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("APPLICATION END!");
            return Task.CompletedTask;
        }
    }
}
