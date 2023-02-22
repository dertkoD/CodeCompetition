using AutoMapper;
using BackEndCompetition.Helpers;
using CompetitionLibrary.Enums;
using CompetitionLibrary.Models;
using CompetitionLibrary.Repositories;
using CompetitionLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCompetition.Controllers
{
    [ApiController]
    [Route("api/Competition/Teams")]
    public class CompetitionTeamController : ControllerBase
    {
        private readonly ILogger<CompetitionTeamController> _logger;
        private readonly IRepository<CompetitionTeam> _dbRepositories;
        private readonly IMapper _mapper;
        private readonly UserHelp _userHelper;

        public CompetitionTeamController (ILogger<CompetitionTeamController> logger, IRepository<CompetitionTeam> dbRepositories, IMapper mapper, UserHelp userHelper)
        {
            _logger = logger;
            _dbRepositories = dbRepositories;
            _mapper = mapper;
            _userHelper = userHelper;
        }

        [HttpGet("{competitionId:int}")]
        public async Task<JsonResult> GetAllTeams(int competitionId)
        {
            try
            {
                var competitionTeams = await _dbRepositories.Where(hackathon => hackathon.CompetitionId == competitionId).GetAll();
                var competitionTeamsDbo = _mapper.Map<List<CompetitionTeamDto>>(competitionTeams);
                _logger.LogInformation("get teams from competition");
                return new JsonResult(Ok(competitionTeamsDbo).Value);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet("{competitionId:int}/{teamId:int}")]
        public async Task<JsonResult> GetTeam(int competitionId, int teamId)
        {
            try
            {
                var competitionTeam = await _dbRepositories.Where(team => team.CompetitionId == competitionId && team.TeamId == teamId).GetOne();
                var competitionTeamDbo = _mapper.Map<CompetitionTeamDto>(competitionTeam);
                _logger.LogInformation("get team from competition");
                return new JsonResult(Ok(competitionTeamDbo).Value);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> AddTeam(CompetitionTeamDto teamDto)
        {
            try
            {
                var team = _mapper.Map<CompetitionTeam>(teamDto);
                team.CreateTime = team.UpdateTime = DateTime.UtcNow;
                team.CreateUserId = team.UpdateUserId = await _userHelper.GetId();
                team.ObjStatusId = (int)EnumStatus.Active;
                await _dbRepositories.Create(team);
                _logger.LogInformation("added team");
                return new JsonResult(Ok());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteTeam(int competitionId, int teamId)
        {
            try
            {
                var team = await _dbRepositories.Where(a => a.TeamId == teamId && a.CompetitionId == competitionId).GetOne();
                await _dbRepositories.Delete(team.CompetitionTeamId, await _userHelper.GetId());
                _logger.LogInformation($"Delete Team for competition {competitionId}");
                return new JsonResult(Ok());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }
    }
}
