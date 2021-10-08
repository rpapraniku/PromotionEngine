using BusinessLogic.Service;
using BusinessLogic.Service.BusinessRules;
using DataAccess.Entities;
using DataAccess.Enums;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PromotionEngineTests.CalculationBusinessLogicTests
{
    public class MultipleBusinessRulesShould
    {
        [Fact]
        public void NotHaveItemForProcessing()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 1, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 1, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 1, SKU = "C", Price = 20 }
                };

            var promotion = new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130 };

            var multipleBusinessRules = new MultipleBusinessRules();

            //Act
            var analizeOrderItemsDTO = multipleBusinessRules.ApplyBusinessRules(orderItems, promotion);

            //Assert
            Assert.Empty(analizeOrderItemsDTO.ItemForProccessing);
        }

        [Fact]
        public void HaveSixItemForProcessing()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 6, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 1, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 3, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 }
                };

            var promotion = new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130 };

            var multipleBusinessRules = new MultipleBusinessRules();

            //Act
            var analizeOrderItemsDTO = multipleBusinessRules.ApplyBusinessRules(orderItems, promotion);

            //Assert
            Assert.Equal(6, analizeOrderItemsDTO.ItemForProccessing.Sum(x => x.Quantity));
        }

        [Fact]
        public void HaveTwoPromotions()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 6, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 1, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 3, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 }
                };

            var promotion = new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130 };

            var multipleBusinessRules = new MultipleBusinessRules();

            //Act
            var analizeOrderItemsDTO = multipleBusinessRules.ApplyBusinessRules(orderItems, promotion);

            //Assert
            Assert.Equal(2, analizeOrderItemsDTO.BundleCount);
        }

        [Fact]
        public void HaveZeroNonPromotionItems()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 6, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 4, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 3, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 }
                };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = BundleType.Multiple, SKU = "A", Quantity = 3, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 130},
                new Promotion { BundleType = BundleType.Multiple, SKU = "B", Quantity = 2 , DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 45}
            };

            var multipleBusinessRules = new MultipleBusinessRules();

            //Act

            foreach (var promotion in promotions)
            {
                var analizeOrderItemsDTO = multipleBusinessRules.ApplyBusinessRules(orderItems, promotion);
                //Assert
                Assert.Empty(analizeOrderItemsDTO.SingleItems);
            }
        }
    }
}
