using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Service.BusinessRules
{
    public class MultipleBusinessRules : ICalculationBusinessLogic
    {
        public bool ValidateOrder(List<OrderItem> orderItems, Promotion promotion)
        {
            return orderItems.Any(x => x.SKU == promotion.SKU);
        }

        public AnalizeOrderItemsDTO ApplyBusinessRules(List<OrderItem> orderItems, Promotion promotion)
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
                x.ItemForProccessing.Add(new OrderItem { Price = item.Price, SKU = item.SKU, Quantity = x.BundleItemCount });
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
