namespace CompetitionLibrary.Models
{
	public class CompetitionTaskCompetDto
	{
		public int CompetitionTaskCompetId { get; set; }

		public int CompetitionId { get; set; }

		public int TaskId { get; set; }

		public TimeSpan? CompetitionTaskTimeComplited { get; set; }

		public int? CompetitionTaskPoint { get; set; }

		public TimeSpan? CompetitionTaskTimeCompleted { get; set; }

		public int? CompetitionTaskPointReceived { get; set; }

		public int CreateUserId { get; set; }

		public int UpdateUserId { get; set; }

		public DateTime CreateTime { get; set; }

		public DateTime UpdateTime { get; set; }
	}
}
