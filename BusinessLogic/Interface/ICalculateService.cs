using BusinessLogic.DTO;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ICalculateService
    {
        public CheckoutSummary CalcualteOrder(Order order, List<Promotion> promotions);
    }
}
