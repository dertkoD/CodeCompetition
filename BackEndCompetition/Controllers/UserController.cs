using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BackEndCompetition.Services.Logging;
using CompetitionLibrary.Services;
using CompetitionLibrary.Repositories;
using CompetitionLibrary.Models;
using static System.Console;
using Microsoft.AspNetCore.Authorization;
using CompetitionLibrary.Enums;

namespace BackEndCompetition.Controllers
{
	[ApiController]
	[Route("api/Users/")]
	[Log]
	public class UserController : ControllerBase
	{
		private readonly ILogger<UserController> _logger;
		private readonly IMapper _mapper;
		private readonly IRepository<User> _dbRepositories;
		private readonly UserHelp _userHelp;
		public UserController(ILogger<UserController> logger, IMapper mapper, IRepository<User> dbRepositories, UserHelp userHelp)
		{
			_logger = logger;
			_mapper = mapper;
			_dbRepositories = dbRepositories;
			_userHelp = userHelp;
		}

		[HttpGet]
		public async Task<JsonResult> GetUsers()
		{
			try
			{
				var userDb = await _dbRepositories.Get(a => a.Team!).GetAll();
				var userAdmins = userDb.Select(user =>
				{
					var a = _mapper.Map<UserDto>(user);
					a.TeamName = user.Team?.TeamName;
					return a;
				}).ToList();
				return new JsonResult(Ok(userAdmins).Value);
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		[HttpGet("{userId:int}")]
		public async Task<JsonResult> GetUserById(int userId)
		{
			try
			{
				var userDb = await _dbRepositories.Get(a => a.Team!).Where(b => b.UserId == userId).GetOne();
				var user = _mapper.Map<UserDto>(userDb);
				user.TeamName = userDb.Team?.TeamName;
				return new JsonResult(Ok(user).Value);

			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<JsonResult> PostUser(UserDto newUserDto)
		{
			try
			{
				var newUser = _mapper.Map<User>(newUserDto);
				newUser.CreateTime = DateTime.UtcNow;
				newUser.UpdateTime = DateTime.UtcNow;
				newUser.ObjStatusId = (int)EnumStatus.Active;
				var user = await _dbRepositories.Create(newUser);
				return new JsonResult(Ok(_mapper.Map<UserDto>(user)));
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		[HttpDelete]
		[Authorize(Roles = "SystemAdmin")]
		public async Task<JsonResult> DeleteUser(int userId)
		{

			try
			{
				await _dbRepositories.Delete(userId, await _userHelp.GetId());
				return new JsonResult(Ok("Object was deleted"));
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		[HttpGet("getId")]
		[Authorize]
		public async Task<JsonResult> GetUserIdByAuth0()
		{
			try
			{
				return new JsonResult(await _userHelp.GetId());
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		[HttpGet("getTeam")]
		[Authorize]
		public async Task<JsonResult> GetTeam()
		{
			try
			{
				var authId = _userHelp.GetAuthId();
				var user = await _dbRepositories.Where(a => a.UserAuth0Id == authId).Get(a => a.Team!).GetOne();
				return new JsonResult(Ok(user.Team?.TeamName));
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		[HttpPut]
		[Authorize]
		public async Task<JsonResult> UpdateUser(UserDto userDto)
		{
			try
			{
				var user = _mapper.Map<User>(userDto);
				user.UpdateTime = DateTime.UtcNow;
				await _dbRepositories.Update(user.UserId, user);
				return new JsonResult(Ok("Update is complete"));
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}
	}
}
