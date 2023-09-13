using System;
using System.Collections.Generic;

namespace NorthwindMVCUygulama.NorthwindDB
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
    }
}
