namespace CompetitionLibrary.Models
{
    public partial class CompetitionTeamTaskStatus
    {
        public int CompetitionTeamTaskStatusId { get; set; }

        public string CompetitionTeamTaskStatusName { get; set; } = null!;

        public virtual ICollection<CompetitionTeamTask> CompetitionTeamTasks { get; } = new List<CompetitionTeamTask>();
    }
}
