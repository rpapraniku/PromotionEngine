﻿using System.Collections.Generic;

namespace BusinessLogic.DTO
{
    public class MultipleBundleItem
    {
        public int Promotions { get; set; }
        public string SKU { get; set; }
        public double PromotionDiscout { get; set; }
        public double Amount { get; set; }
    }
}