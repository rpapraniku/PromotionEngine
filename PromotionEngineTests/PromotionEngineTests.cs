using BusinessLogic.DTO;
using BusinessLogic.DTO.Enums;
using BusinessLogic.Service;
using System.Collections.Generic;
using System.Linq;
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
                    new OrderItem { Quantity = 3, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 2, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 1, SKU = "C", Price = 20 }
                }
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.Percentage, PercentageDiscount = 10},
                new Promotion { BundleType = BundleType.Combination, SKUs = new List<string> { "C", "D" }, DiscountType = DiscountType.Percentage, FixedPriceDiscount = 30 }
            };

            //Act
            var calculateService = new CalculateService();
            var orderResults = calculateService.CalcualteOrder(order, promotions);

            var totalSum = orderResults.SingleItems.Sum(x => x.TotalPrice) +
                orderResults.MultipleBundleItems.Sum(x => x.Amount) +
                orderResults.CombinationBundleItems.Sum(x => x.Amount);

            Assert.Equal(1, orderResults.SingleItems.Sum(x => x.ItemCount));
            Assert.Equal(184, orderResults.MultipleBundleItems.Sum(x => x.Amount));
            Assert.Equal(0, orderResults.CombinationBundleItems.Sum(x => x.Amount));
            Assert.Equal(204, totalSum);
        }

        [Fact]
        public void ScenarioB()
        {
            //Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 5, SKU = "A", Price = 50 }, //MULTIPLE 130 
                    new OrderItem { Quantity = 7, SKU = "B", Price = 30 }, //MULTIPLE 48 + 48 + 48 = 144
                    new OrderItem { Quantity = 4, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 15 } //COMBINE 70 - 20% = 56
                                                                          //SINGLE = 50 + 50 + 30 + 20 + 20
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
            var totalSum = orderResults.SingleItems.Sum(x => x.TotalPrice) +
                orderResults.MultipleBundleItems.Sum(x => x.Amount) +
                orderResults.CombinationBundleItems.Sum(x => x.Amount);

            Assert.Equal(5, orderResults.SingleItems.Sum(x => x.ItemCount));
            Assert.Equal(274, orderResults.MultipleBundleItems.Sum(x => x.Amount));
            Assert.Equal(56, orderResults.CombinationBundleItems.Sum(x => x.Amount));
            Assert.Equal(170, orderResults.SingleItems.Sum(x => x.TotalPrice));
            Assert.Equal(500, totalSum);
        }

        [Fact]
        public void ScenarioC()
        {
            //Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 5, SKU = "A", Price = 50 },// 120
                    new OrderItem { Quantity = 5, SKU = "B", Price = 30 },// 42 + 42 
                    new OrderItem { Quantity = 1, SKU = "C", Price = 20 } // 0
                                                                          // 50 + 50 + 30 + 20
                }
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.Percentage, PercentageDiscount= 20},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.Percentage, PercentageDiscount = 30},
                new Promotion { BundleType = BundleType.Combination, SKUs = new List<string> { "C", "D" }, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 30 }
            };

            //Act
            var calculateService = new CalculateService();
            var orderResults = calculateService.CalcualteOrder(order, promotions);

            //Assert
            //Assert
            var totalSum = orderResults.SingleItems.Sum(x => x.TotalPrice) +
                orderResults.MultipleBundleItems.Sum(x => x.Amount) +
                orderResults.CombinationBundleItems.Sum(x => x.Amount);

            Assert.Equal(4, orderResults.SingleItems.Sum(x => x.ItemCount));
            Assert.Equal(204, orderResults.MultipleBundleItems.Sum(x => x.Amount));
            Assert.Equal(0, orderResults.CombinationBundleItems.Sum(x => x.Amount));
            Assert.Equal(150, orderResults.SingleItems.Sum(x => x.TotalPrice));
            Assert.Equal(354, totalSum);
        }
    }
}
