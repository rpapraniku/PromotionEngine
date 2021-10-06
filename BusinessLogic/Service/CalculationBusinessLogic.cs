using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Service
{
    public class CalculationBusinessLogic : ICalculationBusinessLogic
    {
        public AnalizeOrderItemsDTO AnalizeOrderItems(List<OrderItem> orderItems, Promotion promotion)
        {
            var x = new AnalizeOrderItemsDTO();

            if (promotion.BundleType == DataAccess.Enums.BundleType.Multiple)
            {
                var item = orderItems.FirstOrDefault(x => x.SKU == promotion.SKU);

                x.BundleItemModulus = item.Quantity % promotion.Quantity;
                x.BundleItemCount = item.Quantity - x.BundleItemModulus;
                x.BundleCount = x.BundleItemCount / promotion.Quantity;

                if (x.BundleItemCount > 0)
                {
                    x.ItemForProccessing.Add(new OrderItem { Price = item.Price, SKU = item.SKU, Quantity = item.Quantity });
                }

                if (x.BundleItemModulus > 0)
                {
                    x.SingleItems.Add(new SingleItem { PricePerItem = item.Price, SKU = item.SKU, ItemCount = x.BundleItemModulus, TotalPrice = item.Price * x.BundleItemModulus });
                }
            }
            else
            {
                var items = orderItems.Where(x => promotion.SKUs.Contains(x.SKU)).ToList();

                x.BundleCount = items.Count > 1 ? items.Min(x => x.Quantity) : 0;

                if (x.BundleCount >= 1)
                {
                    foreach (var item in items)
                    {
                        var bundleItemModulus = item.Quantity - x.BundleCount;
                        x.BundleItemCount = item.Quantity - bundleItemModulus;

                        if (bundleItemModulus != 0)
                        {
                            x.SingleItems.Add(new SingleItem
                            {
                                PricePerItem = item.Price,
                                SKU = item.SKU,
                                ItemCount = x.BundleItemModulus,
                                TotalPrice = item.Price * x.BundleItemModulus
                            });
                        }

                        x.ItemForProccessing.Add(new OrderItem { Price = item.Price, SKU = item.SKU, Quantity = x.BundleItemCount });
                    }

                    return x;
                }


                var modulusItem = items.FirstOrDefault();

                if (modulusItem.Quantity > 0)
                {
                    x.SingleItems.Add(new SingleItem
                    {
                        PricePerItem = modulusItem.Price,
                        SKU = modulusItem.SKU,
                        ItemCount = modulusItem.Quantity,
                        TotalPrice = modulusItem.Price * modulusItem.Quantity
                    });
                }
            }

            return x;
        }
    }
}
