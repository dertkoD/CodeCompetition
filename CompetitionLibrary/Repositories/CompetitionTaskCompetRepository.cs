using CompetitionLibrary.Enums;
using CompetitionLibrary.Exeptions;
using CompetitionLibrary.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionLibrary.Repositories
{
    public class CompetitionTaskCompetRepository : SqlRepository<CompetitionTaskCompet>
    {
        private readonly ILogger _logger;

        public CompetitionTaskCompetRepository(DbCompetContext context) : base(context) 
        {
            
        }

        public async Task AssignTask(CompetitionTaskCompet competitionTaskCompet)
        {
            try
            {
                competitionTaskCompet.CreateTime = DateTime.UtcNow;
                competitionTaskCompet.UpdateTime = DateTime.UtcNow;
                competitionTaskCompet.ObjStatusId = (int)EnumStatus.Active;
                if ((await GetOne(competitionTaskCompet.TaskId))!.ObjStatusId == (int)EnumStatus.Delete)
                {
                    throw new NotFoundException($"Task {competitionTaskCompet.TaskId} not found");
                }
                await Create(competitionTaskCompet);
                _logger.LogInformation("Task is successfully created");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                switch (e)
                {
                    case NotFoundException:
                        throw;
                }
                throw new AlreadyExistingException();
            }
        }
    }
}
