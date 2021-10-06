using BusinessLogic.Calculators.Base;
using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Calculators
{
    public class BundleCalculator : CalculateBase
    {
        private readonly Promotion promotion;
        private readonly ICalculationDiscountService _calculationDiscountService;

        public BundleCalculator(Promotion promotion, ICalculationDiscountService calculationDiscountService)
        {
            this.promotion = promotion;
            _calculationDiscountService = calculationDiscountService;
        }

        public override CheckoutSummary Calculate(CheckoutSummary checkoutSummary, List<OrderItem> items)
        {
            var orderItems = items.Where(x => promotion.SKUs.Contains(x.SKU)).ToList();
            var bundleCount = orderItems.Count > 1 ? orderItems.Min(x => x.Quantity) : 0;
            var bundleItemCount = 0;
            var bundleModulusItems = new List<OrderItem>();
            var bundleItemFit = new List<OrderItem>();

            if (bundleCount >= 1)
            {
                foreach (var item in orderItems)
                {
                    var bundleItemModulus = item.Quantity - bundleCount;
                    bundleItemCount = item.Quantity - bundleItemModulus;

                    if (bundleItemModulus != 0)
                    {
                        bundleModulusItems.Add(new OrderItem { Price = item.Price, SKU = item.SKU, Quantity = bundleItemModulus });
                    }

                    bundleItemFit.Add(new OrderItem { Price = item.Price, SKU = item.SKU, Quantity = bundleItemCount });
                }
            }
            else
            {
                foreach (var item in orderItems)
                {
                    if (item.Quantity > 0)
                    {
                        bundleModulusItems.Add(new OrderItem { Price = item.Price, SKU = item.SKU, Quantity = item.Quantity });
                    }
                }
            }


            if (bundleCount > 0)
            {
                var priceAfterDiscount = _calculationDiscountService.CalculateDiscount(promotion.DiscountType, promotion.FixedPriceDiscount, promotion.PercentageDiscount, bundleItemFit.Sum(item => item.Price * item.Quantity), bundleCount, bundleItemCount);

                checkoutSummary.CombinationBundleItems.Add(new CombinationBundleItem
                {
                    BundleCount = orderItems.Sum(x => x.Quantity),
                    SKUs = orderItems.Select(x => x.SKU).ToList(),
                    //PromotionDiscount = priceBeforeDiscount - priceAfterDiscount,
                    Amount = priceAfterDiscount
                });
            }

            if (bundleModulusItems.Count > 0)
            {
                foreach (var item in bundleModulusItems)
                {
                    checkoutSummary.SingleItems.Add(new SingleItem
                    {
                        ItemCount = item.Quantity,
                        PricePerItem = item.Price,
                        TotalPrice = item.Price * item.Quantity,
                        SKU = item.SKU
                    });
                }
            }

            return checkoutSummary;
        }
    }
}
