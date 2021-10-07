using BusinessLogic.DTO;
using BusinessLogic.Service;
using DataAccess.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PromotionEngineTests.CalculationDiscoutServiceTest
{
    public class CalculateDiscountShould
    {
        [Fact]
        public void Have432For20percentTwoBundlePromotions()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 4, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 },

                    new OrderItem { Quantity = 6, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 2, SKU = "Nike", Price = 100 }, //150 -20% = 120 * 2 = 240

                    new OrderItem { Quantity = 3, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "Adidas", Price = 100 }//120 -20% = 96 * 2 = 192
                };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Bundle, SKUs = new List<string> { "A", "Nike" }, DiscountType = DiscountType.Percentage, PercentageDiscount = 20 },
                new Promotion{ BundleType = BundleType.Bundle, SKUs = new List<string> { "C", "Adidas" }, DiscountType = DiscountType.Percentage, PercentageDiscount = 20 }
            };

            var calculationBusinessLogic = new CalculationBusinessLogic();
            var calculationDiscountService = new CalculationDiscountService();

            double totalAmount = 0.0;
            //Act
            foreach (var promotion in promotions)
            {
                var analizeOrderItemsDTO = calculationBusinessLogic.BundleBusinessRules(orderItems, promotion);
                var (priceAferDiscount, priceBeforeDiscont) = calculationDiscountService.BundleCalculateDiscount(analizeOrderItemsDTO, promotion);
                totalAmount += priceAferDiscount;
            }

            //Assert
            Assert.Equal(240 + 192, totalAmount);
        }

        [Fact]
        public void Have760ForTwoFixedPriceBundlePromotions()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 4, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 },

                    new OrderItem { Quantity = 4, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 3, SKU = "Nike", Price = 100 }, //120 * 3 = 360

                    new OrderItem { Quantity = 7, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 4, SKU = "Adidas", Price = 100 }//100 * 4 = 400
                };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Bundle, SKUs = new List<string> { "A", "Nike" }, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 120 },
                new Promotion{ BundleType = BundleType.Bundle, SKUs = new List<string> { "C", "Adidas" }, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 100 }
            };

            var calculationBusinessLogic = new CalculationBusinessLogic();
            var calculationDiscountService = new CalculationDiscountService();

            double totalAferDiscountAmount = 0.0;
            double totalBeforeDiscountAmount = 0.0;

            //Act
            foreach (var promotion in promotions)
            {
                var analizeOrderItemsDTO = calculationBusinessLogic.BundleBusinessRules(orderItems, promotion);
                var (priceAferDiscount, priceBeforeDiscont) = calculationDiscountService.BundleCalculateDiscount(analizeOrderItemsDTO, promotion);

                totalBeforeDiscountAmount += priceBeforeDiscont;
                totalAferDiscountAmount += priceAferDiscount;
            }

            //Assert
            Assert.Equal(480 + 450, totalBeforeDiscountAmount);
            Assert.Equal(360 + 400, totalAferDiscountAmount);
        }


        [Fact]
        public void Have250ForTwoFixedPriceMultiplePromotions()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 4, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 4, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 7, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 },

                    new OrderItem { Quantity = 4, SKU = "Nike", Price = 100 }, // 1 * 250 = 250 
                    new OrderItem { Quantity = 3, SKU = "Adidas", Price = 100 }//1 * 180 = 180
                };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "Nike", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 250},
                new Promotion { BundleType = BundleType.Multiple, SKU = "Adidas", Quantity = 2 , DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 180}
            };

            var calculationBusinessLogic = new CalculationBusinessLogic();
            var calculationDiscountService = new CalculationDiscountService();

            double totalAferDiscountAmount = 0.0;
            double totalBeforeDiscountAmount = 0.0;

            //Act
            foreach (var promotion in promotions)
            {
                var analizeOrderItemsDTO = calculationBusinessLogic.MultipleBusinessRules(orderItems, promotion);
                var (priceAferDiscount, priceBeforeDiscont) = calculationDiscountService.MultipleCalculateDiscount(analizeOrderItemsDTO, promotion);

                totalBeforeDiscountAmount += priceBeforeDiscont;
                totalAferDiscountAmount += priceAferDiscount;
            }

            //Assert
            Assert.Equal(300 + 200, totalBeforeDiscountAmount);
            Assert.Equal(250 + 180, totalAferDiscountAmount);
        }

        [Fact]
        public void Have630ForTwoPercentageDiscountMultiplePromotions()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 4, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 },

                    new OrderItem { Quantity = 4, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 3, SKU = "Nike", Price = 100 }, //3 * 100 = 300 - 10% = 270

                    new OrderItem { Quantity = 7, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 4, SKU = "Adidas", Price = 100 }//100 * 2 * 2 = 400 - 10% = 360 
                };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "Nike", Quantity = 3, DiscountType = DiscountType.Percentage, PercentageDiscount = 10},
                new Promotion { BundleType = BundleType.Multiple, SKU = "Adidas", Quantity = 2 , DiscountType = DiscountType.Percentage, PercentageDiscount= 10}
            };

            var calculationBusinessLogic = new CalculationBusinessLogic();
            var calculationDiscountService = new CalculationDiscountService();

            double totalAferDiscountAmount = 0.0;
            double totalBeforeDiscountAmount = 0.0;

            //Act
            foreach (var promotion in promotions)
            {
                var analizeOrderItemsDTO = calculationBusinessLogic.MultipleBusinessRules(orderItems, promotion);
                var (priceAferDiscount, priceBeforeDiscont) = calculationDiscountService.MultipleCalculateDiscount(analizeOrderItemsDTO, promotion);

                totalBeforeDiscountAmount += priceBeforeDiscont;
                totalAferDiscountAmount += priceAferDiscount;
            }

            //Assert
            Assert.Equal(300 + 400, totalBeforeDiscountAmount);
            Assert.Equal(270 + 360, totalAferDiscountAmount);
        }
    }
}
