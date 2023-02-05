namespace BlazorApp1.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Scores { get; set; }
        public byte[] PhotoTeam { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
