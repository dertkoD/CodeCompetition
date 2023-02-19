namespace CompetitionLibrary.Models
{
    public partial class CompetitionTeamTask
    {
        public int CompetitionTeamTaskId { get; set; }

        public int CompetitionTeamId { get; set; }

        public int CompetitionTaskCompetId { get; set; }

        public DateTime CompetitionTeamTaskStartTime { get; set; }

        public DateTime? CompetitionTeamTaskEndTime { get; set; }

        public int CompetitionTeamTaskStatusId { get; set; }

        public string? CompetitionTeamTaskUrlGitHub { get; set; }

        public int? CompetitionTeamTaskCheckingUserId { get; set; }

        public int CreateUserId { get; set; }

        public int UpdateUserId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int ObjStatusId { get; set; }

        public virtual CompetitionTaskCompet CompetitionTaskCompet { get; set; } = null!;

        public virtual CompetitionTeam CompetitionTeam { get; set; } = null!;

        public virtual User? CompetitionTeamTaskCheckingUser { get; set; }

        public virtual CompetitionTeamTaskStatus CompetitionTeamTaskStatus { get; set; } = null!;

        public virtual User CreateUser { get; set; } = null!;

        public virtual StatusObj ObjStatus { get; set; } = null!;

        public virtual User UpdateUser { get; set; } = null!;
	}
}
