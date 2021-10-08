using BusinessLogic.DTO;
using BusinessLogic.DTO.BundleItemDTO;
using DataAccess.Entities;

namespace BusinessLogic.Interface
{
    public interface ICalculationDiscountService
    {
        BundleItem CalculateDiscount(AnalizeOrderItemsDTO rulesDTO, Promotion promotion);
    }
}
