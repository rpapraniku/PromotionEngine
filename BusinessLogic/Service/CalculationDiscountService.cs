using BusinessLogic.Interface;
using DataAccess.Enums;

namespace BusinessLogic.Service
{
    public class CalculationDiscountService : ICalculationDiscountService
    {
        double priceAfterDiscount = 0.0;
        public double CalculateDiscount(DiscountType discountType, double fixedPrice, double percentage, double price, int bundleCount, int bundleItemCount)
        {
            if (discountType == DiscountType.FixedPrice)
            {
                //priceBeforeDiscount = promotion.Quantity * bundleCount * price;
                priceAfterDiscount = fixedPrice * bundleCount;
            }
            else
            {
                var priceBeforeDiscount = price * bundleItemCount;
                priceAfterDiscount = priceBeforeDiscount - priceBeforeDiscount * percentage / 100;
            }

            return priceAfterDiscount;
        }
    }
}
