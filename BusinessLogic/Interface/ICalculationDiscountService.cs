using DataAccess.Enums;

namespace BusinessLogic.Interface
{
    public interface ICalculationDiscountService
    {
        double CalculateDiscount(DiscountType discountType, double fixedPrice, double percentage, double price, int bundleCount, int bundleItemCount);
    }
}
