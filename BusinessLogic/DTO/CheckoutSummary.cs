using BusinessLogic.DTO.BundleItemDTO;
using System.Collections.Generic;

namespace BusinessLogic.DTO
{
    public class CheckoutSummary
    {
        public List<BundleItem> BundleItems { get; set; }
        public List<SingleItem> SingleItems { get; set; }
        public CheckoutSummary()
        {
            BundleItems = new List<BundleItem>();
            SingleItems = new List<SingleItem>();
        }
    }
}
