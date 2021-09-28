using BusinessLogic.DTO;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface IFacadeService
    {
        string DisplayPromotions();
        public List<OrderItem> GetAllProducts();
        string CalculateOrder(Order order);
    }
}
