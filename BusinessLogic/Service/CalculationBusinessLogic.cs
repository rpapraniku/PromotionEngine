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
        public AnalizeOrderItemsDTO BundleBusinessRules(List<OrderItem> orderItems, Promotion promotion)
        {
            var x = new AnalizeOrderItemsDTO();

            //Get promotion orders
            var items = orderItems.Where(x => promotion.SKUs.Contains(x.SKU)).ToList();

            //calculate bundles; if 1 item found minimum is forced to 0 
            x.BundleCount = items.Count > 1 ? items.Min(x => x.Quantity) : 0;

            //if bundles => calculate
            if (x.BundleCount >= 1)
            {
                foreach (var item in items)
                {
                    var bundleItemModulus = item.Quantity - x.BundleCount;
                    x.BundleItemCount = item.Quantity - bundleItemModulus;

                    //if there are modulus items we insert it for SingleItem (non promotion list)
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

                    //inserting items for calculation discount
                    x.ItemForProccessing.Add(new OrderItem { Price = item.Price, SKU = item.SKU, Quantity = x.BundleItemCount });
                }

                return x;
            }

            //if there is no bundle we check for individual item
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

            return x;
        }

        public AnalizeOrderItemsDTO MultipleBusinessRules(List<OrderItem> orderItems, Promotion promotion)
        {
            var x = new AnalizeOrderItemsDTO();

            //Get promotion orders
            var item = orderItems.FirstOrDefault(x => x.SKU == promotion.SKU);

            x.BundleItemModulus = item.Quantity % promotion.Quantity;
            x.BundleItemCount = item.Quantity - x.BundleItemModulus;
            x.BundleCount = x.BundleItemCount / promotion.Quantity;

            //if there is any bundle we add for furthure processing list
            if (x.BundleCount > 0)
            {
                x.ItemForProccessing.Add(new OrderItem { Price = item.Price, SKU = item.SKU, Quantity = item.Quantity });
            }

            //we also check the modulus and insert to (non promotion list)
            if (x.BundleItemModulus > 0)
            {
                x.SingleItems.Add(new SingleItem { PricePerItem = item.Price, SKU = item.SKU, ItemCount = x.BundleItemModulus, TotalPrice = item.Price * x.BundleItemModulus });
            }

            return x;
        }
    }
}
