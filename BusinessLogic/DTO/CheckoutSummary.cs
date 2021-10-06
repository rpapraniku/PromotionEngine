using BusinessLogic.Calculators.Base;
using System.Collections.Generic;

namespace BusinessLogic.DTO
{
    public class CheckoutSummary
    {
        public List<MultipleBundleItem> MultipleBundleItems { get; set; }
        public List<CombinationBundleItem> CombinationBundleItems { get; set; }
        public List<SingleItem> SingleItems { get; set; }
        public CheckoutSummary()
        {
            MultipleBundleItems = new List<MultipleBundleItem>();
            CombinationBundleItems = new List<CombinationBundleItem>();
            SingleItems = new List<SingleItem>();
        }
    }
}
