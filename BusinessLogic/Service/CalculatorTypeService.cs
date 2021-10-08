using BusinessLogic.Calculators;
using BusinessLogic.Calculators.Base;
using BusinessLogic.Interface;
using BusinessLogic.Service.BusinessRules;
using BusinessLogic.Service.DiscountTypes;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public class CalculatorTypeService : ICalculatorTypeService
    {
        public CalculateBase GetCalculatorType(Promotion promotion)
        {
            if (promotion.BundleType == DataAccess.Enums.BundleType.Bundle)
            {
                return new PromotionCalculator(promotion, new BundleBusinessRules(), new BundleDiscount());
            }
            else
            {
                return new PromotionCalculator(promotion, new MultipleBusinessRules(), new MultipleDiscount());
            }
        }

        public DefaultBase GetDefaultCalculator(List<Promotion> promotions)
        {
            return new DefaultCalculator(promotions);
        }
    }
}
