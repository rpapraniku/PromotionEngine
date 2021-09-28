using BusinessLogic.DTO;
using DataAccess.Entities;
using DataAccess.Enums;

namespace BusinessLogic.Calculators
{
    public static class MultipleCalculator
    {
        public static CheckoutSummary Calculate(CheckoutSummary checkoutSummary, OrderItem item, Promotion promotion)
        {
            var bundleItemModulus = item.Quantity % promotion.Quantity;
            var bundleItemCount = item.Quantity - bundleItemModulus;
            var bundleCount = bundleItemCount / promotion.Quantity;

            if (bundleItemCount > 0)
            {
                var priceAfterDiscount = 0.0;
                var priceBeforeDiscount = 0.0;

                if (promotion.DiscountType == DiscountType.FixedPrice)
                {
                    priceBeforeDiscount = promotion.Quantity * bundleCount * item.Price;
                    priceAfterDiscount = promotion.FixedPriceDiscount * bundleCount;
                }
                else if (promotion.DiscountType == DiscountType.Percentage)
                {
                    priceBeforeDiscount = item.Price * bundleItemCount;
                    priceAfterDiscount = priceBeforeDiscount - priceBeforeDiscount * promotion.PercentageDiscount / 100;
                }

                checkoutSummary.MultipleBundleItems.Add(new MultipleBundleItem
                {
                    Promotions = bundleCount,
                    SKU = item.SKU,
                    PromotionDiscount = priceBeforeDiscount - priceAfterDiscount,
                    Amount = priceAfterDiscount
                });
            }

            var singleItemCount = bundleItemModulus;
            if (singleItemCount > 0)
            {
                checkoutSummary.SingleItems.Add(new SingleItem
                {
                    ItemCount = singleItemCount,
                    PricePerItem = item.Price,
                    TotalPrice = item.Price * singleItemCount,
                    SKU = item.SKU
                });
            }

            return checkoutSummary;
        }
    }
}
