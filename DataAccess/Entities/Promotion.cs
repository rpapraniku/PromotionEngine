using DataAccess.Enums;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public BundleType BundleType { get; set; }
        public string SKU { get; set; }
        public IEnumerable<string> SKUs { get; set; }
        public int Quantity { get; set; }
        public DiscountType DiscountType { get; set; }
        public double FixedPriceDiscount { get; set; }
        public double PercentageDiscount { get; set; }
    }
}