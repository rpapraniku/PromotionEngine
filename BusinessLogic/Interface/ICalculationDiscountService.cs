using BusinessLogic.DTO;
using DataAccess.Entities;
using System;

namespace BusinessLogic.Interface
{
    public interface ICalculationDiscountService
    {
        (double, double) MultipleCalculateDiscount(AnalizeOrderItemsDTO analize, Promotion promotion);

        (double, double) BundleCalculateDiscount(AnalizeOrderItemsDTO analize, Promotion promotion);

    }
}
