using AutoMapper;
using CompetitionLibrary.Enums;
using CompetitionLibrary.Models;

namespace CompetitionLibrary.Services
{
	public class MapConfig : Profile
	{
		public MapConfig()
		{
			CreateMap<CompetitionUserDto, CompetitionUser>().ReverseMap();
			CreateMap<TeamDto, Team>().ReverseMap();
			CreateMap<UserDto, User>().ReverseMap();
			CreateMap<TaskCompetitionDto, TaskCompetition>().ReverseMap();
			CreateMap<Competition, CompetitionDto>()
					  .ForMember(a => a.TasksCompetition, opt => opt
				.MapFrom(src => src.CompetitionTasksCompet.Select(FuncTask).ToList()))
					  .ForMember(a => a.NumberOfUsers, opt =>

					opt.MapFrom(src => src.CompetitionUsers.Count(user => user.ObjStatusId == (int)EnumStatus.Active)))
					  .ReverseMap();
			CreateMap<CompetitionTaskCompetDto, CompetitionTaskCompet>().ReverseMap();
			CreateMap<CompetitionTeam, CompetitionTeamDto>()
					  .ForMember(a => a.Users,
					opt => opt
						.MapFrom(src => src.CompetitionUsers
							.Select(user => user.User))).ReverseMap();
		}

		private static TaskCompetition FuncTask(CompetitionTaskCompet a)
		{
			a.Task.TaskPoint = a.CompetitionTaskPoint ?? a.Task.TaskPoint;
			a.Task.TaskSolutionTime = a.CompetitionTaskSolutionTime ?? a.Task.TaskSolutionTime;
			a.Task.TaskPointReceived = a.CompetitionTaskPointReceived ?? a.Task.TaskPointReceived;
			a.Task.TaskTimeCompleted = a.CompetitionTaskTimeCompleted ?? a.Task.TaskTimeCompleted;
			return a.Task;
		}
	}
}
