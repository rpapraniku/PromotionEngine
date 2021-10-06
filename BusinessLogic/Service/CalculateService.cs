using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public class CalculateService : ICalculateService
    {
        private readonly ICalculatorTypeService _calculatorTypeService;
        public CalculateService(ICalculatorTypeService calculatorTypeService)
        {
            _calculatorTypeService = calculatorTypeService;
        }

        public CheckoutSummary CalcualteOrder(Order order, List<Promotion> promotions)
        {
            var checkoutSummary = new CheckoutSummary();

            foreach (var promotion in promotions)
            {
                checkoutSummary = _calculatorTypeService.CalculateUsingPromotionType(promotion).Calculate(checkoutSummary, order.Items);
            }

            checkoutSummary = _calculatorTypeService.CalculateDefault(promotions).Calculate(checkoutSummary, order.Items);

            return checkoutSummary;
        }
    }
}
