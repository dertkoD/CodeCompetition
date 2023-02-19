using AutoMapper;
using BackEndCompetition.Helpers;
using CompetitionLibrary.Models;
using CompetitionLibrary.Repositories;
using CompetitionLibrary.Services;
using CompetitionLibrary.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCompetition.Controllers
{
    [ApiController]
    [Route("api/Teams/")]
    public class TeamController : ControllerBase
    {
        private readonly IRepository<Team> _dbRepositories;
        private readonly ILogger<TeamController> _logger;
        private readonly IMapper _mapper;
        private readonly UserHelp _userHelper;

        public TeamController (IRepository<Team> dbRepositories, ILogger<TeamController> logger, IMapper mapper, UserHelp userHelper)
        {
            _dbRepositories = dbRepositories;
            _logger = logger;
            _mapper = mapper;
            _userHelper = userHelper;
        }

        [HttpGet("getOne")]
        public async Task<JsonResult> GetTeamByName(string name)
        {
            try
            {
                var team = _mapper.Map<TeamDto>(await _dbRepositories.Where(team => team.TeamName == name)
                    .GetAll());
                return new JsonResult(Ok(team));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTeams()
        {
            try
            {
                var dbTeams = await _dbRepositories.GetAll();
                var teamAdmins = dbTeams.Select(team => _mapper.Map<TeamDto>(team)).ToList();
                return new JsonResult(Ok(teamAdmins).Value);
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPost]
        public async Task<JsonResult> PostTeam(TeamDto newTeamDto)
        {
            try
            {
                var newTeam = _mapper.Map<Team>(newTeamDto);
                newTeam.CreateTime = DateTime.UtcNow;
                newTeam.UpdateTime = DateTime.UtcNow;
                newTeam.ObjStatusId = (int)EnumStatus.Active;
                await _dbRepositories.Create(newTeam);
                return new JsonResult(Ok("Team is added"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteTeam(int teamId)
        {
            try
            {
                await _dbRepositories.Delete(teamId, await _userHelper.GetId());
                return new JsonResult(Ok("Team was deleted"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }

        }

        [HttpPut]
        public async Task<JsonResult> UpdateTeam(TeamDto newTeamDto)
        {
            try
            {
                var team = _mapper.Map<Team>(newTeamDto);
                await _dbRepositories.Update(team.TeamId, team);
                return new JsonResult(Ok("Update is complete"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
    }
}
