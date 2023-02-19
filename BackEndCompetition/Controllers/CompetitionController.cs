using AutoMapper;
using BackEndCompetition.Helpers;
using CompetitionLibrary.Enums;
using CompetitionLibrary.Models;
using CompetitionLibrary.Repositories;
using CompetitionLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackEndCompetition.Controllers
{
    [ApiController]
    [Route("api/Competitions/")]
    public class CompetitionController : ControllerBase
    {
        private readonly ILogger<CompetitionController> _logger;
        private readonly IMapper _mapper;
        private readonly CompetitionRepository _competitionRepositories;
        private readonly IRepository<Competition> _dbRepositories;
        private readonly UserHelp _userHelper;

        public CompetitionController(ILogger<CompetitionController> logger, IMapper mapper, CompetitionRepository competitionRepositories, IRepository<Competition> dbRepositories, UserHelp userHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _competitionRepositories = competitionRepositories;
            _dbRepositories = dbRepositories;
            _userHelper = userHelper;
        }

        [HttpGet("Tasks/{competitionId:int}")]
        public async Task<JsonResult> GetCompetition(int competitionId)
        {
            try
            {
                var @competition = await _competitionRepositories.GetCompetitionTasks(competitionId);
                var competitionDto = _mapper.Map<CompetitionDto>(@competition);
                _logger.LogInformation("competition retrieved from database");
                return new JsonResult(Ok(competitionDto).Value);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet("Users/{competitionId:int}")]
        [Authorize]
        public async Task<JsonResult> GetCompetitionUsers(int competitionId)
        {
            try
            {
                var @competition = await _competitionRepositories.GetCompetitionUsers(competitionId);
                var users = @competition.CompetitionUsers.Select(a =>
                {
                    a.User.CreateTime = a.CreateTime;
                    return _mapper.Map<UserDto>(a.User);
                }).ToList();

                _logger.LogInformation($"User  for competition:{competitionId} retrieved from database");
                return new JsonResult(Ok(users).Value);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet("{competitionId:int}/teams&users")]
        public async Task<JsonResult> GetCompetitionTeamsUsers(int competitionId)
        {
            try
            {
                var competitions = await _competitionRepositories.Get("CompetitionTeams.CompetitionUsers.User", "CompetitionUsers.User")
                    .Where(arg => arg.CompetitionId == competitionId).GetAll();
                var usersWoTeams = (competitions.SelectMany(a => a.CompetitionUsers)
                    .Where(competitionUser => competitionUser.CompetitionTeam == null && competitionUser.ObjStatusId == (int)EnumStatus.Active)
                    .Select(a => a.User).ToList());
                var competitionTeams = competitions.SelectMany(a => a.CompetitionTeams);
                var teamsWithUsers = competitionTeams.Where(a => a.ObjStatusId == (int)EnumStatus.Active).Select(team =>
                {
                    var competitionTeam = _mapper.Map<CompetitionTeamDto>(team);
                    competitionTeam.Users = team.CompetitionUsers.Where(a => a.ObjStatusId == (int)EnumStatus.Active).Select(a => _mapper.Map<UserDto>(a.User)).ToList()!;
                    return competitionTeam;
                }).ToList();
                teamsWithUsers.Add(new CompetitionTeamDto
                {
                    CompetitionTeamId = 0,
                    CompetitionId = 0,
                    TeamId = null,
                    CompetitionTeamName = null,
                    CompetitionTeamAvatar = null,
                    CompetitionTeamPoint = 0,
                    Users = _mapper.Map<List<UserDto>>(usersWoTeams)!
                });
                return new JsonResult(Ok(teamsWithUsers).Value);
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetCompetitions()
        {
            try
            {
                var dbCompetitions = await _competitionRepositories.Get("CompetitionUsers.User").GetAll();
                var userId = _userHelper.GetAuthId()!;
                var competitionDto = dbCompetitions.Select(@competition =>
                {
                    var competitionDto = _mapper.Map<CompetitionDto>(@competition);
                    competitionDto.IsParticipant = @competition.CompetitionUsers.Any(a => a.User.UserAuth0Id == userId && a.ObjStatusId == (int)EnumStatus.Active);
                    return competitionDto;
                }).ToList();
                _logger.LogInformation("Competitions retrieved from database");
                return new JsonResult(Ok(competitionDto).Value);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPost("addCompetition")]
        [Authorize]
        public async Task<JsonResult> PostCompetition(CompetitionDto newCompetitionDto)
        {
            try
            {
                var newCompetition = _mapper.Map<Competition>(newCompetitionDto);
                newCompetition.CreateTime = DateTime.UtcNow;
                newCompetition.UpdateTime = DateTime.UtcNow;
                newCompetition.CompetitionStatusId = (int)StatusCompetition.Created;
                newCompetition.ObjStatusId = (int)EnumStatus.Active;
                await _competitionRepositories.Create(newCompetition);
                _logger.LogInformation("Competition added");
                return new JsonResult(Ok("Competition added"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }


        [HttpDelete]
        [Authorize]
        public async Task<JsonResult> DeleteCompetition(int competitionId)
        {
            try
            {
                await _dbRepositories.Delete(competitionId, await _userHelper.GetId());
                _logger.LogInformation("Competition deleted");
                return new JsonResult(Ok("Competition was deleted"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }
    }
}
