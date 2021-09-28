using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.DTO;
using BusinessLogic.Interface;
using System;
using System.Linq;
using DataAccess.Entities;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = DependencyInjection.ConfigureServices();
            DatabaseSeed.Run(serviceProvider);

            var fasadeService = serviceProvider.GetService<IFacadeService>();
            var products = fasadeService.GetAllProducts();

            Console.WriteLine(fasadeService.DisplayPromotions());
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

            Console.WriteLine(fasadeService.CalculateOrder(order));
            Console.ReadLine();
        }
    }
}
