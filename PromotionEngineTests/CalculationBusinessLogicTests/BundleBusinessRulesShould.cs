using BusinessLogic.Service;
using DataAccess.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PromotionEngineTests.CalculationBusinessLogicTests
{
    public class BundleBusinessRulesShould
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

            var promotion = new Promotion { BundleType = BundleType.Bundle, SKUs = new List<string> { "C", "D" }, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 30 };

            var calculationBusinessLogic = new CalculationBusinessLogic();

            //Act
            var analizeOrderItemsDTO = calculationBusinessLogic.BundleBusinessRules(orderItems, promotion);

            //Assert
            Assert.Empty(analizeOrderItemsDTO.ItemForProccessing);
        }

        [Fact]
        public void HaveTwoItemForProcessing()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 1, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 1, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 3, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 }
                };

            var promotion = new Promotion { BundleType = BundleType.Bundle, SKUs = new List<string> { "C", "D" }, DiscountType = DiscountType.FixedPrice, FixedPriceDiscount = 30 };

            var calculationBusinessLogic = new CalculationBusinessLogic();

            //Act
            var analizeOrderItemsDTO = calculationBusinessLogic.BundleBusinessRules(orderItems, promotion);

            //Assert
            Assert.Equal(2, analizeOrderItemsDTO.ItemForProccessing.Count);
        }
    }
}
