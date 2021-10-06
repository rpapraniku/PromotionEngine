using BusinessLogic.DTO;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ICalculationBusinessLogic
    {
        AnalizeOrderItemsDTO AnalizeOrderItems(List<OrderItem> orderItems, Promotion promotion);
    }
}
