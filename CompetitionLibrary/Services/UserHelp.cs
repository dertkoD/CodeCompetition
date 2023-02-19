using CompetitionLibrary.Models;
using CompetitionLibrary.Repositories;
using Microsoft.AspNetCore.Http;

namespace CompetitionLibrary.Services
{
	public class UserHelp
	{
		private readonly IHttpContextAccessor _context;
		private readonly IRepository<User> _dbRepositories;

		public UserHelp(IHttpContextAccessor context, IRepository<User> dbRepositories)
		{
			_context = context;
			_dbRepositories = dbRepositories;
		}

		public async Task<int> GetId()
		{
			var claim = _context.HttpContext?.User.Claims.FirstOrDefault(a =>
			a.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
			var id = await _dbRepositories.Where(a => a.UserAuth0Id == claim).GetOne();
			return id.UserId;
		}

		public string? GetAuthId()
		{
			var claim = _context.HttpContext?.User.Claims.FirstOrDefault(a =>
			a.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
			return claim;
		}
	}
}
