namespace projCompet.Models
{
    public class CustomerRole
    {
        public Guid CustomerId { get; set; }
        public Customer CustomerUser { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
