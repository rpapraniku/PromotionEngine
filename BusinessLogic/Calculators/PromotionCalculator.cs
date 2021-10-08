using BusinessLogic.Calculators.Base;
using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Calculators
{
    public class PromotionCalculator : PromotionBase
    {
        private readonly Promotion promotion;
        private readonly ICalculationDiscountService _discountService;
        private readonly ICalculationBusinessLogic _calcultationBusinessLogic;

        public PromotionCalculator(Promotion promotion, ICalculationBusinessLogic calculationBusinessLogic, ICalculationDiscountService discountServices) : base(promotion, calculationBusinessLogic, discountServices)
        {
            this.promotion = promotion;
            _calcultationBusinessLogic = calculationBusinessLogic;
            _discountService = discountServices;
        }

        public override CheckoutSummary Calculate(CheckoutSummary checkoutSummary, List<OrderItem> orderItems)
        {
            if (!_calcultationBusinessLogic.ValidateOrder(orderItems, promotion))
            {
                return checkoutSummary;
            }

            var rulesDTO = _calcultationBusinessLogic.ApplyBusinessRules(orderItems, promotion);

            if (rulesDTO.ItemForProccessing.Any())
            {
                checkoutSummary.BundleItems.Add(_discountService.CalculateDiscount(rulesDTO, promotion));
            }

            if (rulesDTO.SingleItems.Any())
            {
                checkoutSummary.SingleItems.AddRange(rulesDTO.SingleItems);
            }

            return checkoutSummary;
        }
    }
}
