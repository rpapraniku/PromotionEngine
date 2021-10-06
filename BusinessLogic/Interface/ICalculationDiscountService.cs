using BusinessLogic.DTO;
using DataAccess.Entities;
using System;

namespace BusinessLogic.Interface
{
    public interface ICalculationDiscountService
    {
        (double, double) CalculateDiscount(AnalizeOrderItemsDTO analize, Promotion promotion);
    }
}
