using BusinessLogic.Calculators;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Enums;
using BusinessLogic.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Service
{
    public class CalculateService : ICalculateService
    {
        public CheckoutSummary CalcualteOrder(Order order, List<Promotion> promotions)
        {
            CheckoutSummary checkoutSummary = new CheckoutSummary();

            foreach (var promotion in promotions)
            {
                if (promotion.BundleType == BundleType.Multiple)
                {
                    var item = order.Items.FirstOrDefault(x => x.SKU == promotion.SKU);
                    if (item == null)
                    {
                        // item not on the bucket list
                        continue;
                    }

                    checkoutSummary = MultipleCalculator.Calculate(checkoutSummary, item, promotion);
                }
                else if (promotion.BundleType == BundleType.Combination)
                {
                    var bundleItems = order.Items.Where(x => promotion.SKUs.Contains(x.SKU)).ToList();
                    if (bundleItems == null)
                    {
                        // item not on the bucket list
                        continue;
                    }

                    checkoutSummary = BundleCalculator.Calculate(checkoutSummary, bundleItems, promotion);
                }
            }

            checkoutSummary = DefaultCalculator.Calculate(checkoutSummary, order.Items, promotions);

            return checkoutSummary;
        }
    }
}
