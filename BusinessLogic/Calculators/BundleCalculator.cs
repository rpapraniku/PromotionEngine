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

            var rulesDTO = _calculationBusinessLogic.BundleBusinessRules(orderItems, promotion);

            if (rulesDTO.ItemForProccessing.Any())
            {
                var (priceAfterDiscount, priceBeforeDiscount) = _calculationDiscountService.CalculateDiscount(rulesDTO, promotion);

                checkoutSummary.CombinationBundleItems.Add(new CombinationBundleItem
                {
                    BundleCount = rulesDTO.ItemForProccessing.Sum(x => x.Quantity),
                    SKUs = rulesDTO.ItemForProccessing.Select(x => x.SKU).ToList(),
                    PromotionDiscount = priceBeforeDiscount - priceAfterDiscount,
                    Amount = priceAfterDiscount
                });
            }

            if (rulesDTO.SingleItems.Any())
            {
                checkoutSummary.SingleItems.AddRange(rulesDTO.SingleItems);
            }

            return checkoutSummary;
        }
    }
}
