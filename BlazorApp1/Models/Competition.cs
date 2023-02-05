namespace BlazorApp1.Models
{
    public class Competition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StatusCompetition Status { get; set; }
        public int NumberOfSimultaneousTasks { get; set; }
        public ICollection<Customer> Admins { get; set; }
        public ICollection<TaskCompetition> Tasks { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
