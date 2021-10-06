using BusinessLogic.Calculators;
using BusinessLogic.Calculators.Base;
using BusinessLogic.Interface;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public class CalculatorTypeService : ICalculatorTypeService
    {
        private readonly ICalculationDiscountService _discountService;
        private readonly ICalculationBusinessLogic _calculationBusinessLogic;

        public CalculatorTypeService(ICalculationBusinessLogic calculationBusinessLogic, ICalculationDiscountService discountService)
        {
            _discountService = discountService;
            _calculationBusinessLogic = calculationBusinessLogic;
        }

        public DefaultBase CalculateDefault(List<Promotion> promotions)
        {
            return new DefaultCalculator(promotions);
        }

        public CalculateBase CalculateUsingPromotionType(Promotion promotion)
        {
            if (promotion.BundleType == DataAccess.Enums.BundleType.Multiple)
            {
                return new MultipleCalculator(promotion, _calculationBusinessLogic, _discountService);
            }
            else
            {
                return new BundleCalculator(promotion, _calculationBusinessLogic, _discountService);
            }
        }
    }
}
