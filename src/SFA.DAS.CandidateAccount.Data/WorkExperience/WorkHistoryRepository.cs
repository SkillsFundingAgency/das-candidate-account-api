using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.WorkExperience
{
    public interface IWorkHistoryRepository
    {
        Task<WorkHistoryEntity> Insert(WorkHistoryEntity workHistoryEntity);
        Task<List<WorkHistoryEntity>> Get(Guid applicationId, CancellationToken cancellationToken);

    }
    public class WorkHistoryRepository(ICandidateAccountDataContext dataContext) : IWorkHistoryRepository
    {
        public async Task<WorkHistoryEntity> Insert(WorkHistoryEntity workHistoryEntity)
        {
            await dataContext.WorkExperienceEntities.AddAsync(workHistoryEntity);
            await dataContext.SaveChangesAsync();
            return workHistoryEntity;
        }

        public async Task<List<WorkHistoryEntity>> Get(Guid applicationId, CancellationToken cancellationToken)
        {
            var query = dataContext
                .WorkExperienceEntities
                .AsNoTracking()
                .Where(fil => fil.ApplicationId == applicationId)
                .OrderBy(a => a.StartDate)
                .ThenBy(a => a.JobTitle);
            return await query.ToListAsync(cancellationToken);
        }
    }
}
