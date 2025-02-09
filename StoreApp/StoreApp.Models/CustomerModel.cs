namespace StoreApp.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstLastName { get; set; }
        public string? SecondLastName { get; set; }
        public string Email { get; set; }

        //Relationships
        public ICollection<OrderModel>? Orders { get; set; }
    }
}
