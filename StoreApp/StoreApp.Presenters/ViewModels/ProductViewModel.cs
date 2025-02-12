namespace StoreApp.Presenters.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitCost { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
