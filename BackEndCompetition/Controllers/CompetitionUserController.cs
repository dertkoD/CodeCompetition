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
                var eventUser = _mapper.Map<CompetitionUser>(competitionUserDto);
                eventUser.CreateTime = DateTime.UtcNow;
                eventUser.UpdateTime = DateTime.UtcNow;
                var userId = await _userHelper.GetId();
                eventUser.CreateUserId = userId;
                eventUser.UpdateUserId = userId;
                eventUser.ObjStatusId = (int)EnumStatus.Active;
                var user = await _competitionUserRepositories.Create(eventUser);
                return new JsonResult(Ok());
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<JsonResult> DeleteUserFromEvent(CompetitionUserDto competitionUserDto)
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
