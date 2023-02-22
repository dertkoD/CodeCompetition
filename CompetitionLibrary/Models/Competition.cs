using CompetitionLibrary.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompetitionLibrary.Models
{
    public partial class Competition : IObjStatus
    {
        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; } = null!;

        public string CompetitionDescription { get; set; } = null!;

        public DateTime CompetitionStartTime { get; set; }

        public DateTime CompetitionEndTime { get; set; }

        public DateTime CompetitionCreatedDate { get; set; }

        public int CompetitionStatusId { get; set; }

        public int? NumberOfUsers { get; set; }

        public int? CompetitionMaxCountOfTeamMembers { get; set; }

        public int? CompetitionMinCountOfTeamMembers { get; set; }

        public int? CompetitionMaxCountOfCompetitionMembers { get; set; }

        public int? CompetitionMinCountOfCompetitionMembers { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int UpdateUserId { get; set; }

        public virtual CompetitionStatus CompetitionStatus { get; set; } = null!;

        public virtual ICollection<CompetitionTaskCompet> CompetitionTasksCompet { get; } = new List<CompetitionTaskCompet>();

        public virtual ICollection<CompetitionTeam> CompetitionTeams { get; } = new List<CompetitionTeam>();

        public virtual ICollection<CompetitionUser> CompetitionUsers { get; } = new List<CompetitionUser>();

        public virtual User CreateUser { get; set; } = null!;

        public virtual User UpdateUser { get; set; } = null!;

        [ForeignKey("ObjStatus")]
        public int ObjStatusId { get; set; }

        public StatusObj ObjStatus { get; set; }
    }
}
