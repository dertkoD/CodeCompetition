namespace CompetitionLibrary.Models
{
    public partial class StatusObj
    {
        public int ObjStatusId { get; set; }

        public string ObjStatusName { get; set; } = null!;

        public virtual ICollection<CompetitionTaskCompet> CompetitionTasksCompets { get; } = new List<CompetitionTaskCompet>();

        public virtual ICollection<CompetitionTeamTask> CompetitionTeamTasks { get; } = new List<CompetitionTeamTask>();

        public virtual ICollection<CompetitionTeam> CompetitionTeams { get; } = new List<CompetitionTeam>();

        public virtual ICollection<CompetitionUser> CompetitionUsers { get; } = new List<CompetitionUser>();

        public virtual ICollection<TaskCompetition> TasksCompetitions { get; } = new List<TaskCompetition>();

        public virtual ICollection<Team> Teams { get; } = new List<Team>();

        public virtual ICollection<User> Users { get; } = new List<User>();
    }
}
