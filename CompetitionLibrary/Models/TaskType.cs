namespace CompetitionLibrary.Models
{
    public partial class TaskType
    {
        public int TaskTypeId { get; set; }

        public string TaskTypeName { get; set; } = null!;

        public virtual ICollection<TaskCompetition> TasksCompetitions { get; } = new List<TaskCompetition>();
    }
}
