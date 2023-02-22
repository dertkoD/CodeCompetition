using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BackEndCompetition.Managment;
using static System.Console;

namespace BackEndCompetition.Controllers
{
	[ApiController]
	[Route("api/management/")]
	public class ManagmentController : ControllerBase
	{
		private readonly IManagementApiClient _managementApiClient;
		private readonly IConfiguration _configuration;
		private readonly Auth0Managment _managementAuth0;

		public ManagmentController (IManagementApiClient managementApiClient, IConfiguration configuration,
			Auth0Managment managementAuth0)
		{
			_managementApiClient = managementApiClient;
			_configuration = configuration;
			_managementAuth0 = managementAuth0;
		}

		[HttpGet]
		[Authorize(Roles = "SystemAdmin")]
		public async Task<JsonResult> GetUsers()
		{
			try
			{
				var users = await _managementApiClient
					.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
				return new JsonResult(users);
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		[HttpGet("getCustomers")]
		[Authorize(Roles = "SystemAdmin")]
		public async Task<JsonResult> GetSimpleUsers(string? roleId)
		{
			try
			{
				roleId ??= _configuration["Roles:Customer"];
				var listOfUsers = await _managementAuth0.GetUsersByRole(roleId!);
				return new JsonResult(listOfUsers);
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		[HttpGet("CompetitionAdmins")]
		[Authorize(Roles = "SystemAdmin")]
		public async Task<JsonResult> GetCompetitionAdmin()
		{
			try
			{
				var listOfUsers = await _managementAuth0.GetUsersByRole(_configuration["Roles:CompetitionAdmin"]!);
				return new JsonResult(listOfUsers);
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		[HttpDelete("deleteRoles/{userId}")]
		[Authorize(Roles = "SystemAdmin")]
		public async Task<JsonResult> DeleteUserRolesByUserId(string userId)
		{
			try
			{
				var rolesToDelete = await _managementApiClient.Users.GetRolesAsync(userId, new PaginationInfo());
				var rolesIdToDeleteStr = rolesToDelete.Select(x => x.Id).ToArray();
				await _managementApiClient.Users.RemoveRolesAsync(userId, new AssignRolesRequest() { Roles = rolesIdToDeleteStr });
				return new JsonResult(NoContent());
			}
			catch (Exception ex)
			{
				WriteLine(ex);
				throw;
			}
		}

		[HttpPost("autoAssign")]
		[Authorize]
		public async Task<JsonResult> AssignRole()
		{
			try
			{
				var claim2 = HttpContext.User.Claims;
				var claim = (claim2?.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier))?.Value.ToString();
				await _managementApiClient.Users.AssignRolesAsync(claim, new AssignRolesRequest() { Roles = new[] { _configuration["Roles:Customer"] } });
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}

			return new JsonResult(Ok());
		}

		[HttpPost("AssignCompetitionAdmin")]
		[Authorize(Roles = "SystemAdmin")]
		public async Task<JsonResult> AssignCompetitionAdmin(string userId)
		{
			try
			{
				await DeleteUserRolesByUserId(userId);
				await _managementApiClient.Users.AssignRolesAsync(userId,
					new AssignRolesRequest() { Roles = new[] { _configuration["Roles:CompetitionAdmin"] } });
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}

			return new JsonResult(NoContent());
		}
	}
}
