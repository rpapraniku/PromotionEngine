using BusinessLogic.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Calculators
{
    public static class DefaultCalculator
    {
        public static CheckoutSummary Calculate(CheckoutSummary checkoutSummary, List<OrderItem> orderItems, List<Promotion> promotions)
        {
            var itemsWithoutPromotion = orderItems.Select(item => item.SKU).Except(promotions.Select(p => p.SKU)).ToList();

            var orderItemsWithoutPromotion = orderItems.Where(x => itemsWithoutPromotion.Contains(x.SKU)).ToList();

            foreach (var item in orderItemsWithoutPromotion)
            {
                checkoutSummary.SingleItems.Add(new SingleItem
                {
                    ItemCount = item.Quantity,
                    PricePerItem = item.Price,
                    TotalPrice = item.Price * item.Quantity,
                    SKU = item.SKU
                });
            }

            return checkoutSummary;
        }
    }
}
