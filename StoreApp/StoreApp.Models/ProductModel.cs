namespace StoreApp.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductEanCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitCost { get; set; }
        public string ImageUrl { get; set; }

        //Relationships
        public ICollection<ProductCategoryModel>? ProductCategories { get; set; }
        public ICollection<OrderItemModel>? OrderItems { get; set; }
        public StockModel Stock { get; set; }

    }
}
