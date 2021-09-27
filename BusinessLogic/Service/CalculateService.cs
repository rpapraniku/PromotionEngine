using BusinessLogic.DTO;
using BusinessLogic.Interface;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public class CalculateService : ICalculateService
    {
        public object CalcualteOrder(Order order, List<Promotion> promotions)
        {
            foreach (var promotion in promotions)
            {
                if (promotion.BundleType == BundleType.Multiple)
                {
                    //logic here
                }
                else if (promotion.BundleType == BundleType.Combination)
                {
                    //logic here
                }
                else
                {

                }
            }
            return new object();
        }
    }
}
