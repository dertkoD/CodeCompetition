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
    [Route("api/competition/user")]
    public class CompetitionUserController : ControllerBase
    {
        private readonly CompetitionUserRepository _competitionUserRepositories;
        private readonly IMapper _mapper;
        private readonly UserHelp _userHelper;

        public CompetitionUserController(CompetitionUserRepository competitionUserRepositories, IMapper mapper, UserHelp userHelper)
        {
            _competitionUserRepositories = competitionUserRepositories;
            _mapper = mapper;
            _userHelper = userHelper;
        }

        [HttpPost]
        public async Task<JsonResult> AddingUser(CompetitionUserDto competitionUserDto)
        {
            try
            {
                var competitionUser = _mapper.Map<CompetitionUser>(competitionUserDto);
                competitionUser.CreateTime = DateTime.UtcNow;
                competitionUser.UpdateTime = DateTime.UtcNow;
                var userId = await _userHelper.GetId();
                competitionUser.CreateUserId = userId;
                competitionUser.UpdateUserId = userId;
                competitionUser.ObjStatusId = (int)EnumStatus.Active;
                var user = await _competitionUserRepositories.Create(competitionUser);
                return new JsonResult(Ok());
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<JsonResult> DeleteUserFromCompetition(CompetitionUserDto competitionUserDto)
        {
            try
            {
                await _competitionUserRepositories.Delete(competitionUserDto.CompetitionId, competitionUserDto.UserId, await _userHelper.GetId());
                return new JsonResult(Ok());
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
    }
}
