namespace projCompet.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CustomerRole> Customers { get; set; }
    }
}
