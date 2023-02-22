using CompetitionLibrary.Models;

namespace CompetitionLibrary.Repositories
{
    public class CompetitionRepository : SqlRepository<Competition>
    {
        private readonly DbCompetContext _context;

        public CompetitionRepository(DbCompetContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Competition> GetCompetitionTasks(int competitionId)
        {
            var dbCompetitions = await Get("CompetitionTasksCompet.Task")
                .Where(a => a.CompetitionId == competitionId).GetOne();
            return dbCompetitions;
        }

        public async Task<Competition> GetCompetitionUsers(int competitionId)
        {
            var dbCompetitions = await Get("CompetitionUsers.User")
                .Where(a => a.CompetitionId == competitionId).GetOne();
            return dbCompetitions;
        }
    }
}
