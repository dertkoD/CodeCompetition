using System.ComponentModel.DataAnnotations;

namespace CompetitionLibrary.Models
{
    public class UserDto
	{
		public int UserId { get; set; }

		[Required(ErrorMessage = "Fill in the First name field\r\n")]
		[StringLength(20, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 20 characters")]
		public string UserFirstName { get; set; } = null!;

		[Required(ErrorMessage = "Fill in the Second name field\r\n")]
		[StringLength(20, MinimumLength = 3, ErrorMessage = "Second name must be between 3 and 20 characters")]
		public string UserSecondName { get; set; } = null!;

		public string UserEmail { get; set; } = null!;

		public string UserAvatar { get; set; } = null!;

		public int? TeamId { get; set; }

		public DateTime CreateTime { get; set; }

		public DateTime UpdateTime { get; set; }

		public int UpdateUserId { get; set; }

		public DateTime UserBirthday { get; set; }

		public string? UserGitHub { get; set; } = null!;

		public string UserAuth0Id { get; set; } = null!;

		public string? TeamName { get; set; }
	}
}