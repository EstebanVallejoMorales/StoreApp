namespace StoreApp.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitCost { get; set; }
        public string ImageUrl { get; set; }
        public List<Category> Categories { get; set; }
    }
}
