namespace CompetitionLibrary.Models
{
	public partial class CompetitionUserCompetTeamTask
	{
		public int CompetitionUserId { get; set; }

		public int CompetitionTeamTaskId { get; set; }

		public virtual CompetitionTeamTask CompetitionTeamTask { get; set; } = null!;

		public virtual CompetitionUser CompetitionUser { get; set; } = null!;
	}
}
