using System.ComponentModel.DataAnnotations;

namespace CompetitionLibrary.Models
{
	public class CompetitionDto
	{
		public int CompetitionId { get; set; }

		[Required(ErrorMessage = "Fill in the Competition name field\r\n")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = "Competition name must be between 3 and 100 characters")]
		public string CompetitionName { get; set; } = null!;

		[Required(ErrorMessage = "Fill in the description name field\r\n")]
		[StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
		public string CompetitionDescription { get; set; } = null!;

		[Required(ErrorMessage = "Fill in the start time  field\r\n")]
		public DateTime CompetitionStartTime { get; set; }

		[Required(ErrorMessage = "Fill in the end time  field\r\n")]
		public DateTime CompetitionEndTime { get; set; }

		[Required(ErrorMessage = "Fill in the publication date time  field\r\n")]
		public DateTime CompetitionCreatedDate { get; set; }

		public int CompetitionStatusId { get; set; }

		public int? NumberOfUsers { get; set; }

		public int? CompetitionMaxCountOfTeamMembers { get; set; }

		public int? CompetitionMinCountOfTeamMembers { get; set; }

		public int? CompetitionMaxCountOfCompetitionMembers { get; set; }

		public int? CompetitionMinCountOfCompetitionMembers { get; set; }

		public int CreateUserId { get; set; }

		public int UpdateUserId { get; set; }

		public DateTime CreateTime { get; set; }

		public DateTime UpdateTime { get; set; }

		public bool IsParticipant { get; set; }
		public ICollection<TaskCompetitionDto>? TasksCompetition { get; set; } = new List<TaskCompetitionDto>();

		public ICollection<UserDto>? Users { get; set; } = new List<UserDto>();
	}
}
