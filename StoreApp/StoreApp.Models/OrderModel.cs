namespace StoreApp.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public int StatusId { get; set; }
        public StatusModel Status { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
    }
}
