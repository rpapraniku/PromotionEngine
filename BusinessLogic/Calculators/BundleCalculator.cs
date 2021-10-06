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
        private readonly ICalculationBusinessLogic _calculationBusinessLogic;

        public BundleCalculator(Promotion promotion, ICalculationBusinessLogic calculationBusinessLogic, ICalculationDiscountService calculationDiscountService)
        {
            this.promotion = promotion;
            _calculationDiscountService = calculationDiscountService;
            _calculationBusinessLogic = calculationBusinessLogic;
        }

        public override CheckoutSummary Calculate(CheckoutSummary checkoutSummary, List<OrderItem> orderItems)
        {
            if (!orderItems.Any(x => promotion.SKUs.Contains(x.SKU)) || orderItems == null)
            {
                return checkoutSummary;
            }

            var analize = _calculationBusinessLogic.AnalizeOrderItems(orderItems, promotion);

            if (analize.ItemForProccessing.Any())
            {
                var (priceAfterDiscount, priceBeforeDiscount) = _calculationDiscountService.CalculateDiscount(analize, promotion);

                checkoutSummary.CombinationBundleItems.Add(new CombinationBundleItem
                {
                    BundleCount = analize.ItemForProccessing.Sum(x => x.Quantity),
                    SKUs = analize.ItemForProccessing.Select(x => x.SKU).ToList(),
                    PromotionDiscount = priceBeforeDiscount - priceAfterDiscount,
                    Amount = priceAfterDiscount
                });
            }

            if (analize.SingleItems.Any())
            {
                checkoutSummary.SingleItems.AddRange(analize.SingleItems);
            }

            return checkoutSummary;
        }
    }
}
