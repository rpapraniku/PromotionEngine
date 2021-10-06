using BusinessLogic.DTO;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Calculators.Base
{
    public abstract class CalculateBase
    {
        public abstract CheckoutSummary Calculate(CheckoutSummary checkoutSummary, List<OrderItem> orderItems);
    }
}
