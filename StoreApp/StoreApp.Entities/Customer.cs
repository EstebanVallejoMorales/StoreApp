namespace StoreApp.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstLastName { get; set; }
        public string? SecondLastName { get; set; }
        public string Email { get; set; }
    }
}
