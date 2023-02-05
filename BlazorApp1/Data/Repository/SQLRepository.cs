using BlazorApp1.Models;

namespace BlazorApp1.Data.Repository
{
    public class SQLRepository : IRepository
    {
        private readonly DB _context;
        public SQLRepository(DB context) 
        {
            _context= context;
        }

        #region CategoriesTask
        public IEnumerable<CategoryTask> GetAllCategoriesTask()
        {
            return _context.CategoryTask;
        }

        public void AddCategoriesTask(string name, string description, TimeSpan? extraTime, int? extraPoint)
        {
            CategoryTask categoryTask = new CategoryTask()
            {
                Name = name,
                Description = description,
                ExtraTime = extraTime,
                ExtraPoints= extraPoint
            };

            _context.CategoryTask.Add(categoryTask);
            _context.SaveChanges();
        }

        public void DeleteCategoriesTask(Guid id)
        {
            var deletedCategoryTask = _context.CategoryTask.Find(id);

            if (deletedCategoryTask != null)
            {
                _context.CategoryTask.Remove(deletedCategoryTask);
                _context.SaveChanges();
            }
        }

        public void UpdateCategoriesTask(CategoryTask categoryTaskChanged)
        {
            var updateCategoriesTask = _context.CategoryTask.Attach(categoryTaskChanged);
            updateCategoriesTask.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
        }
        #endregion

        #region Competition
        public IEnumerable<Competition> GetAllCompetitions()
        {
            return _context.Competition;
        }

        public void AddCompetition(string name, StatusCompetition status, int numberOfSimultaneousTasks, 
            ICollection<Customer> admins, ICollection<TaskCompetition> tasks, ICollection<Team> teams)
        {
            Competition competition = new Competition()
            {
                Name = name,
                Status = status,
                NumberOfSimultaneousTasks = numberOfSimultaneousTasks,
                Admins = admins,
                Tasks = tasks,
                Teams = teams
            };

            _context.Competition.Add(competition);
            _context.SaveChanges();
        }

        public void DeleteCompetition(Guid id)
        {
            var deletedCompetition = _context.Competition.Find(id);

            if (deletedCompetition != null)
            {
                _context.Competition.Remove(deletedCompetition);
                _context.SaveChanges();
            }
        }

        public void UpdateCompetition(Competition competitionChanged)
        {
            var updateCompetition = _context.Competition.Attach(competitionChanged);
            updateCompetition.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
        }
        #endregion

        #region Customers
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customer;
        }

        public void AddCustomer(string firstName, string lastName, string email, int age, ICollection<CustomerRole> roles)
        {
            Customer customer = new Customer()
            {
                Id = new Guid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Age = age,
                Roles = roles
            };

            _context.Customer.Add(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Guid id)
        {
            var deletedCustomer = _context.Customer.Find(id);

            if (deletedCustomer != null)
            {
                _context.Customer.Remove(deletedCustomer);
                _context.SaveChanges();
            }
        }

        public void UpdateCustomer(Customer customerChanged)
        {
            var updateCustomer = _context.Customer.Attach(customerChanged);
            updateCustomer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
        }
        #endregion

        #region CustomerRoles
        public IEnumerable<CustomerRole> GetAllIdCustomersRoles()
        {
            return _context.CustomerRole;
        }

        public void AddIdCustomersAndRoles(Guid customerId, Guid roleId)
        {
            CustomerRole customerRole = new CustomerRole()
            {
                CustomerId = customerId,
                RoleId = roleId
            };

            _context.CustomerRole.Add(customerRole);
            _context.SaveChanges();
        }

        public void DeleteIdCustomersAndRoles(Guid customerId, Guid roleId)
        {
            var deleteIdCustomersAndRoles = _context.CustomerRole.Find(customerId, roleId);

            if (deleteIdCustomersAndRoles != null)
            {
                _context.CustomerRole.Add(deleteIdCustomersAndRoles);
                _context.SaveChanges();
            }
        }
        #endregion

        #region Roles
        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Role;
        }

        public void AddRole(string name, ICollection<CustomerRole> customers)
        {
            Role role = new Role()
            {
                Id = new Guid(),
                Name = name,
                Customers = customers
            };

            _context.Role.Add(role);
            _context.SaveChanges();
        }

        public void DeleteRole(Guid id)
        {
            var deletedRole = _context.Role.Find(id);

            if (deletedRole != null)
            {
                _context.Role.Remove(deletedRole);
                _context.SaveChanges();
            }
        }

        public void UpdateRole(Role roleChanged)
        {
            var updateRole = _context.Role.Attach(roleChanged);
            updateRole.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
        }
        #endregion

        #region TasksCometition
        public IEnumerable<TaskCompetition> GetAllTasksCompetition()
        {
            return _context.TaskCompetition;
        }

        public void AddTasksCompetition(string name, string description, CategoryTask category, string? decision, 
            int point, TimeSpan completionTime, Team? teamBelongs, ICollection<Customer>? customersPerformeTask, 
            DateTime? timeStart, DateTime timeEnd)
        {
            TaskCompetition taskCompetition = new TaskCompetition()
            {
                Name = name,
                Description = description,
                Category = category,
                Decision = decision,
                Point = point,
                CompletionTime = completionTime,
                TeamBelongs = teamBelongs,
                CustomersPerformeTask = customersPerformeTask,
                TimeStart = timeStart,
                TimeEnd = timeEnd
            };

            _context.TaskCompetition.Add(taskCompetition);
            _context.SaveChanges();
        }

        public void DeleteTasksCompetition(Guid id)
        {
            var deletedTasksCompetition = _context.TaskCompetition.Find(id);

            if (deletedTasksCompetition != null)
            {
                _context.TaskCompetition.Remove(deletedTasksCompetition);
                _context.SaveChanges();
            }
        }

        public void UpdateTasksCompetition(TaskCompetition taskCompetitionChanged)
        {
            var updateTasksCompetition = _context.TaskCompetition.Attach(taskCompetitionChanged);
            updateTasksCompetition.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
        }
        #endregion

        #region Teams
        public IEnumerable<Team> GetAllTeams()
        {
            return _context.Team;
        }

        public void AddTeam(string name, int scores, byte[] photoTeam, ICollection<Customer> customers)
        {
            Team team = new Team()
            {
                Name= name,
                Scores = scores,
                PhotoTeam = photoTeam,
                Customers = customers
            };

            _context.Team.Add(team);
            _context.SaveChanges();
        }

        public void DeleteTeam(Guid teamId)
        {
            var deletedTeam = _context.Team.Find(teamId);

            if (deletedTeam != null)
            {
                _context.Team.Remove(deletedTeam);
                _context.SaveChanges();
            }
        }

        public void UpdateTeam(Team teamChanged)
        {
            var updateTeam = _context.Team.Attach(teamChanged);
            updateTeam.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
        }
        #endregion

        #region StatusesCompetition
        public IEnumerable<StatusCompetition> GetAllStatusesCompetition()
        {
            return _context.StatusCompetition;
        }
        #endregion
    }
}
