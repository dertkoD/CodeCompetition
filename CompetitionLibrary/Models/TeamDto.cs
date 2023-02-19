namespace CompetitionLibrary.Models
{
	public class TeamDto
	{
		public int TeamId { get; set; }

		public string TeamName { get; set; } = null!;

		public string? TeamAvatar { get; set; }

		public int CreateUserId { get; set; }

		public int UpdateUserId { get; set; }

		public DateTime? CreateTime { get; set; }

		public DateTime? UpdateTime { get; set; }
	}
}
