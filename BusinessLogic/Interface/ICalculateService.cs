using BusinessLogic.DTO;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ICalculateService
    {
        public object CalcualteOrder(Order order, List<Promotion> promotions);
    }
}
