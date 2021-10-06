using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Calculators.Base
{
    public abstract class DefaultBase : CalculateBase
    {
        public List<Promotion> Promotions { get; set; }

        public DefaultBase(List<Promotion> promotions)
        {
            Promotions = promotions;
        }
    }
}
