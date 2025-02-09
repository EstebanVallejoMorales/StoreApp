namespace StoreApp.Models
{
    public class ProductCategoryModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
