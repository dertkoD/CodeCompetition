using CompetitionLibrary.Repositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompetitionLibrary.Models
{
    public partial class Team : IObjStatus
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TeamId { get; set; }

        public string TeamName { get; set; } = null!;

        public string? TeamAvatar { get; set; }

        public int CreateUserId { get; set; }

        public int UpdateUserId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int ObjStatusId { get; set; }

        public virtual ICollection<CompetitionTeam> CompetitionTeams { get; } = new List<CompetitionTeam>();

        public virtual User CreateUser { get; set; } = null!;

        public virtual StatusObj ObjStatus { get; set; } = null!;

        public virtual User UpdateUser { get; set; } = null!;

        public virtual ICollection<User> Users { get; } = new List<User>();
    }
}