using System.ComponentModel.DataAnnotations;

namespace CompetitionLibrary.Models
{
	public class TaskCompetitionDto
	{
		public int TaskId { get; set; }

		public int TaskTypeId { get; set; }

		[Required(ErrorMessage = "Fill in the task name field\r\n")]
		[StringLength(100, MinimumLength = 4, ErrorMessage = "Task name must be between 4 and 100 characters")]
		public string TaskName { get; set; } = null!;

		[Required(ErrorMessage = "Fill in the description field\r\n")]
		[StringLength(1000, MinimumLength = 10, ErrorMessage = "Descriprion must be between 10 and 1000 characters")]
		public string TaskDescription { get; set; } = null!;

		public TimeSpan TaskSolutionTime { get; set; }

		public int TaskPoint { get; set; }

		public TimeSpan? TaskTimeCompleted { get; set; }

		public int? TaskPointReceived { get; set; }

		public int? MainTaskId { get; set; }

		public int CreateUserId { get; set; }

		public int UpdateUserId { get; set; }

		public DateTime CreateTime { get; set; }

		public DateTime UpdateTime { get; set; }
	}
}
