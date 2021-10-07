using BusinessLogic.Service;
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

            var calculationBusinessLogic = new CalculationBusinessLogic();

            //Act
            var analizeOrderItemsDTO = calculationBusinessLogic.MultipleBusinessRules(orderItems, promotion);

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

            var calculationBusinessLogic = new CalculationBusinessLogic();

            //Act
            var analizeOrderItemsDTO = calculationBusinessLogic.MultipleBusinessRules(orderItems, promotion);

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

            var calculationBusinessLogic = new CalculationBusinessLogic();

            //Act
            var analizeOrderItemsDTO = calculationBusinessLogic.MultipleBusinessRules(orderItems, promotion);

            //Assert
            Assert.Equal(2, analizeOrderItemsDTO.BundleCount);
        }
    }
}
