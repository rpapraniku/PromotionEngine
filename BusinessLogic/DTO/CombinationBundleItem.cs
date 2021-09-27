using System.Collections.Generic;

namespace BusinessLogic.DTO
{
    public class CombinationBundleItem
    {
        public int BundleCount { get; set; }
        public List<string> SKUs { get; set; }
        public double PromotionDiscout { get; set; }
        public double Amount { get; set; }
    }
}