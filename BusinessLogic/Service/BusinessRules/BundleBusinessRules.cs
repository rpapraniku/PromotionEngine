using BusinessLogic.Calculators.Base;
using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Service.BusinessRules
{
    public class BundleBusinessRules : ICalculationBusinessLogic
    {
        public bool ValidateOrder(List<OrderItem> orderItems, Promotion promotion)
        {
            return orderItems.Any(x => promotion.SKUs.Contains(x.SKU));
        }
        public AnalizeOrderItemsDTO ApplyBusinessRules(List<OrderItem> orderItems, Promotion promotion)
        {
            var x = new AnalizeOrderItemsDTO();

            //Get promotion orders
            var items = orderItems.Where(x => promotion.SKUs.Contains(x.SKU)).ToList();

            //calculate bundles; if 1 item found minimum is forced to 0 
            x.BundleCount = items.Count > 1 ? items.Min(x => x.Quantity) : 0;

            //if bundles => calculate
            if (x.BundleCount > 0)
            {
                foreach (var item in items)
                {
                    var bundleItemModulus = item.Quantity - x.BundleCount;
                    var bundleItemCount = item.Quantity - bundleItemModulus;

                    //if there are modulus items we insert it for SingleItem (non promotion list)
                    if (bundleItemModulus != 0)
                    {
                        x.SingleItems.Add(new SingleItem
                        {
                            PricePerItem = item.Price,
                            SKU = item.SKU,
                            ItemCount = bundleItemModulus,
                            TotalPrice = item.Price * bundleItemModulus
                        });
                    }

                    //inserting items for calculation discount
                    x.ItemForProccessing.Add(new OrderItem { Price = item.Price, SKU = item.SKU, Quantity = bundleItemCount });
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
    }
}
