using BusinessLogic.DTO;
using BusinessLogic.Service;
using DataAccess.Entities;
using DataAccess.Enums;
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
                    new OrderItem { Quantity = 1, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 1, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 1, SKU = "C", Price = 20 }
                }
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 45},
                new Promotion { BundleType = BundleType.Combination, SKUs = new List<string> { "C", "D" }, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 30 }
            };

            //Act
            var calculateService = new CalculateService();
            var orderResults = calculateService.CalcualteOrder(order, promotions);

            var totalSum = orderResults.SingleItems.Sum(x => x.TotalPrice) +
                orderResults.MultipleBundleItems.Sum(x => x.Amount) +
                orderResults.CombinationBundleItems.Sum(x => x.Amount);

            Assert.Equal(3, orderResults.SingleItems.Sum(x => x.ItemCount));
            Assert.Equal(0, orderResults.MultipleBundleItems.Sum(x => x.Amount));
            Assert.Equal(0, orderResults.CombinationBundleItems.Sum(x => x.Amount));
            Assert.Equal(100, totalSum);
        }

        [Fact]
        public void ScenarioB()
        {
            //Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 5, SKU = "A", Price = 50 }, // 130 
                    new OrderItem { Quantity = 5, SKU = "B", Price = 30 }, // 45 + 45 = 90
                    new OrderItem { Quantity = 1, SKU = "C", Price = 20 }
                }                                                          //Items Without Promotion 50 + 50 + 30 + 20 = 150
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 45},
                new Promotion { BundleType = BundleType.Combination, SKUs = new List<string> { "C", "D" }, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 30 }
            };

            //Act
            var calculateService = new CalculateService();
            var orderResults = calculateService.CalcualteOrder(order, promotions);

            //Assert
            var totalSum = orderResults.SingleItems.Sum(x => x.TotalPrice) +
                orderResults.MultipleBundleItems.Sum(x => x.Amount) +
                orderResults.CombinationBundleItems.Sum(x => x.Amount);

            Assert.Equal(4, orderResults.SingleItems.Sum(x => x.ItemCount));
            Assert.Equal(220, orderResults.MultipleBundleItems.Sum(x => x.Amount));
            Assert.Equal(0, orderResults.CombinationBundleItems.Sum(x => x.Amount));
            Assert.Equal(150, orderResults.SingleItems.Sum(x => x.TotalPrice));
            Assert.Equal(370, totalSum);
        }

        [Fact]
        public void ScenarioC()
        {
            //Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 3, SKU = "A", Price = 50 },// 130
                    new OrderItem { Quantity = 5, SKU = "B", Price = 30 },// 45 + 45
                    new OrderItem { Quantity = 1, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 1, SKU = "D", Price = 15 } // 30
                                                                          //Items Without Promotion 30
                }
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 45},
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

            Assert.Equal(1, orderResults.SingleItems.Sum(x => x.ItemCount));
            Assert.Equal(220, orderResults.MultipleBundleItems.Sum(x => x.Amount));
            Assert.Equal(30, orderResults.CombinationBundleItems.Sum(x => x.Amount));
            Assert.Equal(30, orderResults.SingleItems.Sum(x => x.TotalPrice));
            Assert.Equal(280, totalSum);
        }

        #region My tests
        
        [Fact]
        public void ScenarioD()
        {
            //Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 0, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 0, SKU = "D", Price = 15 }
                }
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.Percentage, PercentageDiscount = 20},
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

            Assert.Equal(0, orderResults.SingleItems.Sum(x => x.ItemCount));
            Assert.Equal(0, orderResults.MultipleBundleItems.Sum(x => x.Amount));
            Assert.Equal(0, orderResults.CombinationBundleItems.Sum(x => x.Amount));
            Assert.Equal(0, orderResults.SingleItems.Sum(x => x.TotalPrice));
            Assert.Equal(0, totalSum);
        }

        [Fact]
        public void ScenarioE()
        {
            //Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 0, SKU = "A", Price = 20 },
                    new OrderItem { Quantity = 0, SKU = "B", Price = 15 }
                }
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.Percentage, PercentageDiscount = 20},
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

            Assert.Equal(0, orderResults.SingleItems.Sum(x => x.ItemCount));
            Assert.Equal(0, orderResults.MultipleBundleItems.Sum(x => x.Amount));
            Assert.Equal(0, orderResults.CombinationBundleItems.Sum(x => x.Amount));
            Assert.Equal(0, orderResults.SingleItems.Sum(x => x.TotalPrice));
            Assert.Equal(0, totalSum);
        }

        [Fact]
        public void ScenarioF()
        {
            //Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 6, SKU = "A", Price = 50 },// 120
                    new OrderItem { Quantity = 4, SKU = "B", Price = 30 },// 42 + 42 
                    new OrderItem { Quantity = 2, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 15 },// 0
                                                                          // 50 + 50 + 30 + 20
                }
            };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.Percentage, PercentageDiscount = 20},
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

            //Assert.Equal(4, orderResults.SingleItems.Sum(x => x.ItemCount));
            //Assert.Equal(204, orderResults.MultipleBundleItems.Sum(x => x.Amount));
            //Assert.Equal(0, orderResults.CombinationBundleItems.Sum(x => x.Amount));
            //Assert.Equal(150, orderResults.SingleItems.Sum(x => x.TotalPrice));
            Assert.Equal(416, totalSum);
        }

        #endregion
    }
}
