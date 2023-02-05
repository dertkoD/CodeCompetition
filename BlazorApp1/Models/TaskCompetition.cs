namespace BlazorApp1.Models
{
    public class TaskCompetition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryTask Category { get; set; }
        public string? Decision { get; set; }
        public int Point { get; set; }
        public TimeSpan CompletionTime { get; set; }
        public Team? TeamBelongs { get; set; }
        public ICollection<Customer>? CustomersPerformeTask { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
    }
}
