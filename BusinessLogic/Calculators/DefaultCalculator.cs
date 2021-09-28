using BusinessLogic.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Calculators
{
    public static class DefaultCalculator
    {
        public static CheckoutSummary Calculate(CheckoutSummary checkoutSummary, List<OrderItem> orderItems, List<Promotion> promotions)
        {
            var multiplePromotion = promotions.Where(p => !string.IsNullOrWhiteSpace(p.SKU)).Select(x => x.SKU).ToList();
            var combinePromotions = promotions.Where(p => p.SKUs != null).SelectMany(x => x.SKUs).ToList();
            var allPromotios = multiplePromotion.Union(combinePromotions).ToList();

            var itemsWithoutPromotion = orderItems.Select(item => item.SKU).Except(allPromotios.Select(sku => sku)).ToList();
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
