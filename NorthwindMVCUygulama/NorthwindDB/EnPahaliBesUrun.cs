using System;
using System.Collections.Generic;

namespace NorthwindMVCUygulama.NorthwindDB
{
    public partial class EnPahaliBesUrun
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
    }
}
