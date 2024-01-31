using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.WorkExperience
{
    public interface IWorkHistoryRepository
    {
        Task<WorkHistoryEntity> Insert(WorkHistoryEntity workHistoryEntity);
        Task<List<WorkHistoryEntity>> Get(Guid applicationId, Guid candidateId, CancellationToken cancellationToken);

    }
    public class WorkHistoryRepository(ICandidateAccountDataContext dataContext) : IWorkHistoryRepository
    {
        public async Task<WorkHistoryEntity> Insert(WorkHistoryEntity workHistoryEntity)
        {
            await dataContext.WorkExperienceEntities.AddAsync(workHistoryEntity);
            await dataContext.SaveChangesAsync();
            return workHistoryEntity;
        }

        public async Task<List<WorkHistoryEntity>> Get(Guid applicationId, Guid candidateId, CancellationToken cancellationToken)
        {
            var query = from wrk in dataContext.WorkExperienceEntities.Where(fil => fil.ApplicationId == applicationId)
                    .OrderBy(a => a.StartDate)
                    .ThenBy(a => a.JobTitle)
                        join application in dataContext.ApplicationEntities.Where(fil => fil.CandidateId == candidateId && fil.Id == applicationId)
                            on wrk.ApplicationId equals application.Id
                        select wrk;

            return await query.ToListAsync(cancellationToken);
        }
    }
}
