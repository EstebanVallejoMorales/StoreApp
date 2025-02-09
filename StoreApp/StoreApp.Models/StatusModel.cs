namespace StoreApp.Models
{
    public class StatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
    }
}
