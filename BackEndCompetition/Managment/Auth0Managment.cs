using Auth0.ManagementApi;
using CompetitionLibrary.Models;
using CompetitionLibrary.Repositories;

namespace BackEndCompetition.Managment
{
	public class Auth0Managment
	{
		private readonly IManagementApiClient _managementApiClient;
		private readonly IRepository<User> _irepository;

		public Auth0Managment(IManagementApiClient managementApiClient, IRepository<User> irepository)
		{
			_managementApiClient = managementApiClient;
			_irepository = irepository;
		}

		public async Task<List<User>> GetUsersByRole(string roleId)
		{
			try
			{
				var assignedUsers = await _managementApiClient.Roles.GetUsersAsync(roleId);
				var listOfIds = assignedUsers.Select(user => user.UserId).ToList();
				var listOfUsers = await _irepository.Where(a => listOfIds.Contains(a.UserAuth0Id)).GetAll();
				return listOfUsers;
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}
