using CompetitionLibrary.Repositories;
namespace CompetitionLibrary.Models
{
    public partial class CompetitionUser : IObjStatus
    {
        public int CompetitionUserId { get; set; }

        public int CompetitionId { get; set; }

        public int UserId { get; set; }

        public int? CompetitionTeamId { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int ObjStatusId { get; set; }

        public int UpdateUserId { get; set; }

        public virtual Competition Competition { get; set; } = null!;

        public virtual CompetitionTeam? CompetitionTeam { get; set; }

        public virtual User CreateUser { get; set; } = null!;

        public virtual StatusObj ObjStatus { get; set; } = null!;

        public virtual User UpdateUser { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}