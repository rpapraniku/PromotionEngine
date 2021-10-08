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

            var totalSum = checkoutSummary.SingleItems.Sum(x => x.TotalPrice) +
                checkoutSummary.BundleItems.Sum(x => x.Amount);

            var totaldiscount = checkoutSummary.BundleItems.Sum(x => x.PromotionDiscount);

            Console.WriteLine($"Promotion discount:{ totaldiscount }");
            Console.WriteLine($"Total Amount:{ totalSum }");

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
