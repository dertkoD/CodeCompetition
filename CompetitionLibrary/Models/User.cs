using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CompetitionLibrary.Repositories;

namespace CompetitionLibrary.Models
{
    public partial class User : IObjStatus
	{
		[Key]

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }

        public string UserFirstName { get; set; } = null!;

        public string UserSecondName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string UserAvatar { get; set; } = null!;

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int ObjStatusId { get; set; }

        public DateTime UserBirthday { get; set; }

        public bool UserBirthdayVisibility { get; set; }

        public string? UserGitHub { get; set; }

        public int? TeamId { get; set; }

        public string UserAuth0Id { get; set; } = null!;

        public virtual ICollection<Competition> CompetitionCreateUsers { get; } = new List<Competition>();

        public virtual ICollection<CompetitionTaskCompet> CompetitionTasksCompetCreateUsers { get; } = new List<CompetitionTaskCompet>();

        public virtual ICollection<CompetitionTaskCompet> CompetitionTasksCompetUpdateUsers { get; } = new List<CompetitionTaskCompet>();

        public virtual ICollection<CompetitionTeam> CompetitionTeamCreateUsers { get; } = new List<CompetitionTeam>();

        public virtual ICollection<CompetitionTeamTask> CompetitionTeamTaskCompetitionTeamTaskCheckingUsers { get; } = new List<CompetitionTeamTask>();

        public virtual ICollection<CompetitionTeamTask> CompetitionTeamTaskCreateUsers { get; } = new List<CompetitionTeamTask>();

        public virtual ICollection<CompetitionTeamTask> CompetitionTeamTaskUpdateUsers { get; } = new List<CompetitionTeamTask>();

        public virtual ICollection<CompetitionTeam> CompetitionTeamUpdateUsers { get; } = new List<CompetitionTeam>();

        public virtual ICollection<Competition> CompetitionUpdateUsers { get; } = new List<Competition>();

        public virtual ICollection<CompetitionUser> CompetitionUserCreateUsers { get; } = new List<CompetitionUser>();

        public virtual ICollection<CompetitionUser> CompetitionUserUpdateUsers { get; } = new List<CompetitionUser>();

        public virtual ICollection<CompetitionUser> CompetitionUserUsers { get; } = new List<CompetitionUser>();

        public virtual StatusObj ObjStatus { get; set; } = null!;

        public virtual ICollection<TaskCompetition> TasksCompetitionCreateUsers { get; } = new List<TaskCompetition>();

        public virtual ICollection<TaskCompetition> TasksCompetitionUpdateUsers { get; } = new List<TaskCompetition>();

        public virtual Team? Team { get; set; }

        public virtual ICollection<Team> TeamCreateUsers { get; } = new List<Team>();

        public virtual ICollection<Team> TeamUpdateUsers { get; } = new List<Team>();
		public int UpdateUserId { get; set; }
	}
}
