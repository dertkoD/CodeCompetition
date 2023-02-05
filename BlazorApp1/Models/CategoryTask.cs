namespace BlazorApp1.Models
{
    public class CategoryTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan? ExtraTime { get; set; }
        public int? ExtraPoints { get; set; }
    }
}
