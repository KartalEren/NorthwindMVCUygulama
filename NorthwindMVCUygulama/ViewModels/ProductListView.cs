namespace NorthwindMVCUygulama.ViewModels
{
    public class ProductListView
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
    }
}
