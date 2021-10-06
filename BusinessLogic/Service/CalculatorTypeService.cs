using BusinessLogic.Calculators;
using BusinessLogic.Calculators.Base;
using BusinessLogic.Interface;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public class CalculatorTypeService : ICalculatorTypeService
    {
        ICalculationDiscountService _discountService;

        public CalculatorTypeService(ICalculationDiscountService discountService)
        {
            _discountService = discountService;
        }

        public DefaultBase CalculateDefault(List<Promotion> promotions)
        {
            return new DefaultCalculator(promotions);
        }

        public CalculateBase CalculateUsingPromotionType(Promotion promotion)
        {
            if (promotion.BundleType == DataAccess.Enums.BundleType.Multiple)
            {
                return new MultipleCalculator(promotion, _discountService);
            }
            else
            {
                return new BundleCalculator(promotion, _discountService);
            }
        }
    }
}
