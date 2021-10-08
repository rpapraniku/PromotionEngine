using BusinessLogic.DTO;
using BusinessLogic.DTO.BundleItemDTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using DataAccess.Enums;
using System.Linq;

namespace BusinessLogic.Service.DiscountTypes
{
    public class MultipleDiscount : ICalculationDiscountService
    {
        public BundleItem CalculateDiscount(AnalizeOrderItemsDTO rulesDTO, Promotion promotion)
        {
            double priceBeforeDiscount = 0.0;
            double priceAfterDiscount = 0.0;

            if (promotion.DiscountType == DiscountType.FixedPrice)
            {
                priceBeforeDiscount = promotion.Quantity * rulesDTO.BundleCount * rulesDTO.ItemForProccessing.First().Price;
                priceAfterDiscount = promotion.FixedPriceDiscount * rulesDTO.BundleCount;
            }
            else
            {
                priceBeforeDiscount = rulesDTO.ItemForProccessing.Sum(item => item.Price * item.Quantity);
                priceAfterDiscount = priceBeforeDiscount - priceBeforeDiscount * promotion.PercentageDiscount / 100;
            }

            return new MultipleBundleItem()
            {
                DiscountType = promotion.DiscountType,
                BundleCount = rulesDTO.BundleCount,
                SKU = rulesDTO.ItemForProccessing.FirstOrDefault().SKU,
                PromotionDiscount = priceBeforeDiscount - priceAfterDiscount,
                Amount = priceAfterDiscount
            };
        }
    }
}
