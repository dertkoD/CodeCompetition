using CompetitionLibrary.Enums;
using CompetitionLibrary.Models;
using CompetitionLibrary.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionLibrary.Repositories
{
    public class CompetitionUserRepository : SqlRepository<CompetitionUser>, IRepository<CompetitionUser>
    {
        private readonly DbCompetContext _context;

        public CompetitionUserRepository(DbCompetContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(int competitionId, int userIdDelete, int userId)
        {
            try
            {
                var entry = await Where(a => a.CompetitionId == competitionId, b => b.UserId == userIdDelete).GetOne();
                if (entry != null)
                {
                    entry.ObjStatusId = (int)EnumStatus.Delete;
                    entry.UpdateUserId = userId;
                    entry.UpdateTime = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new NotFoundException();
            }
        }

        public async Task<CompetitionUser> Create(CompetitionUser model)
        {
            var existingModel = _context.CompetitionUsers.FirstOrDefault(a => a.CompetitionId == model.CompetitionId && a.UserId == model.UserId);
            if (existingModel != null)
            {
                existingModel.ObjStatusId = (int)EnumStatus.Active;
                await _context.SaveChangesAsync();
                return existingModel;
            };
            return await base.Create(model);
        }
    }
}
