using CompetitionLibrary.Repositories;

namespace CompetitionLibrary.Models
{
    public partial class CompetitionTaskCompet : IObjStatus
    {
        public int CompetitionTaskCompetId { get; set; }

        public int CompetitionId { get; set; }

        public int TaskId { get; set; }

        public TimeSpan? CompetitionTaskSolutionTime { get; set; }

        public int? CompetitionTaskPoint { get; set; }

        public TimeSpan? CompetitionTaskTimeCompleted { get; set; }

        public int? CompetitionTaskPointReceived { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int CreateUserId { get; set; }

        public int ObjStatusId { get; set; }

        public int UpdateUserId { get; set; }

        public virtual Competition Competition { get; set; } = null!;

        public virtual ICollection<CompetitionTeamTask> CompetitionTeamTasks { get; } = new List<CompetitionTeamTask>();

        public virtual User CreateUser { get; set; } = null!;

        public virtual StatusObj ObjStatus { get; set; } = null!;

        public virtual TaskCompetition Task { get; set; } = null!;

        public virtual User UpdateUser { get; set; } = null!;
    }
}
