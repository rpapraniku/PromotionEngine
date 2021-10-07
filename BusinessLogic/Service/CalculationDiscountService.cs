using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using DataAccess.Enums;
using System.Linq;

namespace BusinessLogic.Service
{
    public class CalculationDiscountService : ICalculationDiscountService
    {
        double priceAfterDiscount = 0.0;
        double priceBeforeDiscount = 0.0;

        public (double, double) BundleCalculateDiscount(AnalizeOrderItemsDTO analize, Promotion promotion)
        {
            if (promotion.DiscountType == DiscountType.FixedPrice)
            {
                priceBeforeDiscount = analize.ItemForProccessing.Sum(item => item.Price * item.Quantity);
                priceAfterDiscount = promotion.FixedPriceDiscount * analize.BundleCount;
            }
            else
            {
                priceBeforeDiscount = analize.ItemForProccessing.Sum(item => item.Price * item.Quantity);
                priceAfterDiscount = priceBeforeDiscount - priceBeforeDiscount * promotion.PercentageDiscount / 100;
            }

            return (priceAfterDiscount, priceBeforeDiscount);
        }

        public (double, double) MultipleCalculateDiscount(AnalizeOrderItemsDTO analize, Promotion promotion)
        {
            if (promotion.DiscountType == DiscountType.FixedPrice)
            {
                priceBeforeDiscount = promotion.Quantity * analize.BundleCount * analize.ItemForProccessing.First().Price;
                priceAfterDiscount = promotion.FixedPriceDiscount * analize.BundleCount;
            }
            else
            {
                priceBeforeDiscount = analize.ItemForProccessing.Sum(item => item.Price * item.Quantity);
                priceAfterDiscount = priceBeforeDiscount - priceBeforeDiscount * promotion.PercentageDiscount / 100;
            }

            return (priceAfterDiscount, priceBeforeDiscount);
        }
    }
}
