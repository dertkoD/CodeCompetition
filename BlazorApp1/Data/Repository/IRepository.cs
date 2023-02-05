using BlazorApp1.Models;

namespace BlazorApp1.Data.Repository
{
    public interface IRepository
    {
        #region Customers
        IEnumerable<Customer> GetAllCustomers();
        void AddCustomer(string firstName, string lastName, string email, int age, ICollection<CustomerRole> roles);
        void DeleteCustomer(Guid id);
        void UpdateCustomer(Customer customerChanged);
        #endregion

        #region Roles
        IEnumerable<Role> GetAllRoles();
        void AddRole(string name, ICollection<CustomerRole> customers);
        void DeleteRole(Guid id);
        void UpdateRole(Role roleChanged);
        #endregion

        #region CustomerRoles
        IEnumerable<CustomerRole> GetAllIdCustomersRoles();
        void AddIdCustomersAndRoles(Guid customerId, Guid roleId);
        void DeleteIdCustomersAndRoles(Guid customerId, Guid roleId);
        #endregion

        #region CategoriesTask
        IEnumerable<CategoryTask> GetAllCategoriesTask();
        void AddCategoriesTask(string name, string description, TimeSpan? extraTime, int? extraPoint);
        void DeleteCategoriesTask(Guid id);
        void UpdateCategoriesTask(CategoryTask categoryTaskChanged);
        #endregion

        #region TasksCometition
        IEnumerable<TaskCompetition> GetAllTasksCompetition();
        void AddTasksCompetition(string name, string description, CategoryTask category, string? decision, int point, TimeSpan completionTime,
            Team? teamBelongs, ICollection<Customer>? customersPerformeTask, DateTime? timeStart, DateTime timeEnd);
        void DeleteTasksCompetition(Guid id);
        void UpdateTasksCompetition(TaskCompetition taskCompetitionChanged);
        #endregion

        #region Teams
        IEnumerable<Team> GetAllTeams();
        void AddTeam(string name, int scores, byte[] photoTeam, ICollection<Customer> customers);
        void DeleteTeam(Guid teamId);
        void UpdateTeam(Team teamChanged);
        #endregion

        #region StatusesCompetition
        IEnumerable<StatusCompetition> GetAllStatusesCompetition();
        #endregion

        #region Competition
        IEnumerable<Competition> GetAllCompetitions();
        void AddCompetition(string name, StatusCompetition status, int numberOfSimultaneousTasks, ICollection<Customer> admins,
            ICollection<TaskCompetition> tasks, ICollection<Team> teams);
        void DeleteCompetition(Guid id);
        void UpdateCompetition(Competition competitionChanged);
        #endregion
    }
}
