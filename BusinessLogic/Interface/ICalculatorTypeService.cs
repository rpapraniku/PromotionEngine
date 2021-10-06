using BusinessLogic.Calculators.Base;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ICalculatorTypeService
    {
        CalculateBase CalculateUsingPromotionType(Promotion promotion);

        DefaultBase CalculateDefault(List<Promotion> promotions);
    }
}
