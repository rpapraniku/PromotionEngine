using DataAccess;
using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class DatabaseSeed
    {
        public static void Run(ServiceProvider serviceProvider)
        {

            using (var context = new InMemoryDbContext(serviceProvider.GetRequiredService<DbContextOptions<InMemoryDbContext>>()))
            {
                try
                {
                    context.OrderItems.AddRange(
                        new OrderItem { Id = 1, SKU = "A", Price = 50 },
                        new OrderItem { Id = 2, SKU = "B", Price = 30 },
                        new OrderItem { Id = 3, SKU = "C", Price = 20 },
                        new OrderItem { Id = 4, SKU = "D", Price = 15 });


                    context.Promotions.AddRange(
                        new Promotion
                        {
                            Id = 1,
                            Title = "3 of A's for 130",
                            BundleType = BundleType.Multiple,
                            SKU = "A",
                            Quantity = 3,
                            DiscountType = DiscountType.FixedPrice,
                            FixedPriceDiscount = 130
                        },
                        new Promotion
                        {
                            Id = 2,
                            Title = "2 of B's for 45",
                            BundleType = BundleType.Multiple,
                            SKU = "B",
                            Quantity = 2,
                            DiscountType = DiscountType.Percentage,
                            PercentageDiscount = 10
                        },
                        new Promotion
                        {
                            Id = 3,
                            Title = "C & D for 30",
                            BundleType = BundleType.Combination,
                            SKUs = new List<string> { "C", "D" },
                            DiscountType = DiscountType.Percentage,
                            FixedPriceDiscount = 30
                        });



                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}