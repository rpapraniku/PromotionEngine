using BusinessLogic.DTO;
using BusinessLogic.Interface;
using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Service
{
    public class FacadeService : IFacadeService
    {
        private readonly InMemoryDbContext _context;
        private readonly ICalculateService _calculateService;

        public FacadeService(ICalculateService calculateService, InMemoryDbContext context)
        {
            _context = context;
            _calculateService = calculateService;
        }

        public string CalculateOrder(Order order)
        {
            var checkoutSummary = _calculateService.CalcualteOrder(order, _context.Promotions.ToList());

            var displayResult = string.Empty;

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("------------------------");
            Console.WriteLine("SKU\t\tQty\t\tAmount");
            Console.WriteLine("------------------------");

            //Console.Write($"{checkoutSummary.SingleItems.Select(x => x.SKU)}\t\t{product.Quantity}\t\t{product.Amount}\t\t{product.PromotionApplied}{Environment.NewLine}");
            //Console.Write($"{checkoutSummary.MultipleBundleItems.Select(x => x.SKU)}\t\t{product.Quantity}\t\t{product.Amount}\t\t{product.PromotionApplied}{Environment.NewLine}");
            //Console.Write($"{product.PromotionFormula}\t\t{product.Quantity}\t\t{product.Amount}\t\t{product.PromotionApplied}{Environment.NewLine}");

            var totalSum = checkoutSummary.SingleItems.Sum(x => x.TotalPrice) +
                checkoutSummary.MultipleBundleItems.Sum(x => x.Amount) +
                checkoutSummary.CombinationBundleItems.Sum(x => x.Amount);

            Console.Write($"{Environment.NewLine}\t\t\t\t  Total Amount: { totalSum }");

            return displayResult;
        }

        public string DisplayPromotions()
        {
            var promotions = _context.Promotions.ToList();
            string promotionMessage = promotions.Any() ? "Active Promotions:" + Environment.NewLine : string.Empty;

            foreach (var promotion in promotions)
            {
                promotionMessage += promotion.Title + Environment.NewLine;
            }

            return promotionMessage;
        }

        public List<OrderItem> GetAllProducts()
        {
            return _context.OrderItems.ToList();
        }
    }
}
