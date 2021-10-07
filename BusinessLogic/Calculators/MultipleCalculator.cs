﻿using BusinessLogic.Calculators.Base;
using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Calculators
{
    public class MultipleCalculator : CalculateBase
    {
        private readonly Promotion promotion;
        private readonly ICalculationDiscountService _calculationDiscountService;
        private readonly ICalculationBusinessLogic _calculationBusinessLogic;

        public MultipleCalculator(Promotion promotion, ICalculationBusinessLogic calculationBusinessLogic, ICalculationDiscountService calculationDiscountService)
        {
            _calculationDiscountService = calculationDiscountService;
            _calculationBusinessLogic = calculationBusinessLogic;
            this.promotion = promotion;
        }

        public override CheckoutSummary Calculate(CheckoutSummary checkoutSummary, List<OrderItem> orderItems)
        {
            if (!orderItems.Any(x => x.SKU == promotion.SKU))
            {
                return checkoutSummary;
            }

            var rulesDTO = _calculationBusinessLogic.MultipleBusinessRules(orderItems, promotion);

            if (rulesDTO.ItemForProccessing.Any())
            {
                var (priceAfterDiscount, priceBeforeDiscount) = _calculationDiscountService.CalculateDiscount(rulesDTO, promotion);

                checkoutSummary.MultipleBundleItems.Add(new MultipleBundleItem
                {
                    Promotions = rulesDTO.BundleCount,
                    SKU = rulesDTO.ItemForProccessing.FirstOrDefault().SKU,
                    PromotionDiscount = priceBeforeDiscount - priceAfterDiscount,
                    Amount = priceAfterDiscount
                }); ;
            }

            if (rulesDTO.SingleItems.Any())
            {
                checkoutSummary.SingleItems.AddRange(rulesDTO.SingleItems);
            }

            return checkoutSummary;
        }
    }
}
