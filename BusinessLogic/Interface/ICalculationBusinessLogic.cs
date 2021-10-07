using BusinessLogic.DTO;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ICalculationBusinessLogic
    {
        AnalizeOrderItemsDTO BundleBusinessRules(List<OrderItem> orderItems, Promotion promotion);
        AnalizeOrderItemsDTO MultipleBusinessRules(List<OrderItem> orderItems, Promotion promotion);

    }
}
