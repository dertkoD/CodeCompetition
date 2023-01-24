namespace projCompet.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName{ get; set; }
        public int Age { get; set; }
        public ICollection<CustomerRole> Roles { get; set; }
    }
}
