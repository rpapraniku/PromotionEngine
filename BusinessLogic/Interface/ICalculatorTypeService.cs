using BusinessLogic.Calculators.Base;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ICalculatorTypeService
    {
        CalculateBase GetCalculatorType(Promotion promotion);
        DefaultBase GetDefaultCalculator(List<Promotion> promotions);
    }
}
