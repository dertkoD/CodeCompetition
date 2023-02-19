using AutoMapper;
using BackEndCompetition.Helpers;
using CompetitionLibrary.Models;
using CompetitionLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCompetition.Controllers
{
    [ApiController]
    [Route("api/Competition/Task")]
    public class CompetitionTaskCompetController : ControllerBase
    {
        private readonly ILogger<CompetitionTaskCompetController> _logger;
        private readonly IMapper _mapper;
        private readonly CompetitionTaskCompetRepository _competitionTaskCompetRepositories;

        public CompetitionTaskCompetController (ILogger<CompetitionTaskCompetController> logger, IMapper mapper, CompetitionTaskCompetRepository competitionTaskCompetRepositories)
        {
            _logger = logger;
            _mapper = mapper;
            _competitionTaskCompetRepositories = competitionTaskCompetRepositories;
        }

        [HttpPost]
        public async Task<JsonResult> AssignMission(CompetitionTaskCompetDto newCompetitionTaskCompet)
        {
            try
            {
                var competitionTaskCompet = _mapper.Map<CompetitionTaskCompet>(newCompetitionTaskCompet);
                await _competitionTaskCompetRepositories.AssignMission(competitionTaskCompet);
                return new JsonResult(Ok("Task assigned"));

            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
    }
}
