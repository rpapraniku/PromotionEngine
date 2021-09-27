using BusinessLogic.DTO;
using BusinessLogic.Service;
using System.Collections.Generic;
using Xunit;

namespace PromotionEngineTests
{
    public class PromotionEngineTests
    {
        [Fact]
        public void ScenarioA()
        {
            //Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 1, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 1, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 1, SKU = "C", Price = 20 }
                }
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 50},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.Percentage, PercentageDiscount = 10},
                new Promotion { BundleType = BundleType.Combination, SKUs = new List<string> { "C", "D" }, DiscountType = DiscountType.Percentage, FixedPriceDiscount = 30 }
            };

            //Act
            var calculateService = new CalculateService();
            var orderResults = calculateService.CalcualteOrder(order, promotions);

            //Assert
            //Assert.Equal(100,  )

        }

        [Fact]
        public void ScenarioB()
        {
            //Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 5, SKU = "A", Price = 50 }, // 130 + 50 + 50
                    new OrderItem { Quantity = 5, SKU = "B", Price = 30 }, // 48 + 48 + 30
                    new OrderItem { Quantity = 1, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 1, SKU = "D", Price = 15 } // 28
                }
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.Percentage, PercentageDiscount = 20},
                new Promotion { BundleType = BundleType.Combination, SKUs = new List<string> { "C", "D" }, FixedPriceDiscount = 20, DiscountType = DiscountType.Percentage }
            };

            //Act
            var calculateService = new CalculateService();
            var orderResults = calculateService.CalcualteOrder(order, promotions);

            //Assert
        }

        [Fact]
        public void ScenarioC()
        {
            //Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 5, SKU = "A", Price = 50 },// 130 + 50 + 50
                    new OrderItem { Quantity = 5, SKU = "B", Price = 30 },// 48 + 48 + 30
                    new OrderItem { Quantity = 3, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 15 }
                }
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.Percentage, PercentageDiscount = 20},
                new Promotion { BundleType = BundleType.Combination, SKUs = new List<string> { "C", "D" }, DiscountType = DiscountType.Percentage, PercentageDiscount = 20 }
            };

            //Act
            var calculateService = new CalculateService();
            var orderResults = calculateService.CalcualteOrder(order, promotions);

            //Assert
        }
    }
}
