using BusinessLogic.Calculators.Base;
using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Calculators
{
    public class MultipleCalculator : CalculateBase
    {
        private readonly Promotion promotion;
        private readonly ICalculationDiscountService _calculationDiscountService;

        public MultipleCalculator(Promotion promotion, ICalculationDiscountService calculationDiscountService)
        {
            _calculationDiscountService = calculationDiscountService;
            this.promotion = promotion;
        }

        public override CheckoutSummary Calculate(CheckoutSummary checkoutSummary, List<OrderItem> orderItems)
        {
            var item = orderItems.FirstOrDefault(x => x.SKU == promotion.SKU);

            if (item == null)
            {
                return checkoutSummary;
            }

            var bundleItemModulus = item.Quantity % promotion.Quantity;
            var bundleItemCount = item.Quantity - bundleItemModulus;
            var bundleCount = bundleItemCount / promotion.Quantity;

            if (bundleItemCount > 0)
            {
                var priceAfterDiscount = _calculationDiscountService.CalculateDiscount(promotion.DiscountType, promotion.FixedPriceDiscount, promotion.PercentageDiscount, item.Price, bundleCount, bundleItemCount);

                checkoutSummary.MultipleBundleItems.Add(new MultipleBundleItem
                {
                    Promotions = bundleCount,
                    SKU = item.SKU,
                    //PromotionDiscount = priceBeforeDiscount - priceAfterDiscount,
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
