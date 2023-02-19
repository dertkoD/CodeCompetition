namespace CompetitionLibrary.Models
{
	public class CompetitionTeamDto
	{
		public int CompetitionTeamId { get; set; }

		public int CompetitionId { get; set; }

		public int? TeamId { get; set; }

		public string CompetitionTeamName { get; set; } = null!;

		public string CompetitionTeamAvatar { get; set; } = null!;

		public bool TeamTwitterPoint { get; set; }

		public int CompetitionTeamPoint { get; set; }

		public List<UserDto> Users { get; set; } = null!;
	}
}
