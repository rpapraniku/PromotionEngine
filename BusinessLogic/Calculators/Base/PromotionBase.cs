using BusinessLogic.Interface;
using DataAccess.Entities;

namespace BusinessLogic.Calculators.Base
{
    public abstract class PromotionBase : CalculateBase
    {
        private Promotion promotion;
        public PromotionBase(Promotion promotion, ICalculationBusinessLogic calculationBusinessLogic, ICalculationDiscountService discountServices)
        {
            this.promotion = promotion;
        }
    }
}
