using System.Collections.Generic;

namespace PromotionEngine
{
    internal class Promotion
    {
        public string BundleType { get; set; }
        public string SKU { get; set; }
        public List<string> SKUs { get; set; }
        public int MultipleBundleCount { get; set; }
        public string DiscountType { get; set; }
        public double FixedPriceDiscount { get; set; }
        public double PercentageDiscount { get; set; }
    }
}