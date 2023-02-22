using CompetitionLibrary.Repositories;
namespace CompetitionLibrary.Models
{
    public partial class CompetitionTeam : IObjStatus
    {
        public int CompetitionTeamId { get; set; }

        public int CompetitionId { get; set; }

        public int? TeamId { get; set; }

        public string CompetitionTeamName { get; set; } = null!;

        public string CompetitionTeamAvatar { get; set; } = null!;

        public int CreateUserId { get; set; }

        public int UpdateUserId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int ObjStatusId { get; set; }

        public int CompetitionTeamPoint { get; set; }

        public virtual Competition Competition { get; set; } = null!;

        public virtual ICollection<CompetitionTeamTask> CompetitionTeamTasks { get; } = new List<CompetitionTeamTask>();

        public virtual ICollection<CompetitionUser> CompetitionUsers { get; } = new List<CompetitionUser>();

        public virtual User CreateUser { get; set; } = null!;

        public virtual StatusObj ObjStatus { get; set; } = null!;

        public virtual Team? Team { get; set; }

        public virtual User UpdateUser { get; set; } = null!;
    }
}
