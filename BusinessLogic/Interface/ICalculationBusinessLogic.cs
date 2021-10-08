using BusinessLogic.DTO;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ICalculationBusinessLogic
    {
        bool ValidateOrder(List<OrderItem> orderItems, Promotion promotion);
        AnalizeOrderItemsDTO ApplyBusinessRules(List<OrderItem> orderItems, Promotion promotion);
    }
}
