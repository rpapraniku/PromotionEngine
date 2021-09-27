using System.Collections.Generic;

namespace BusinessLogic.DTO
{
    public class Promotion
    {
        public BundleType BundleType { get; set; }
        public string SKU { get; set; }
        public List<string> SKUs { get; set; }
        public int Quantity { get; set; }
        public DiscountType DiscountType { get; set; }
        public double FixedPriceDiscount { get; set; }
        public double PercentageDiscount { get; set; }
    }
}