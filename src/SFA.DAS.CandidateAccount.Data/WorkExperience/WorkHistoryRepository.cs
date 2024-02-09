using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.WorkExperience
{
    public interface IWorkHistoryRepository
    {
        Task<WorkHistoryEntity> Insert(WorkHistoryEntity workHistoryEntity);
        Task Update(WorkHistoryEntity workHistoryEntity);
        Task<WorkHistoryEntity?> Get(Guid applicationId, Guid candidateId, Guid id, WorkHistoryType? workHistoryType, CancellationToken cancellationToken);
        Task<List<WorkHistoryEntity>> GetAll(Guid applicationId, Guid candidateId, WorkHistoryType? workHistoryType, CancellationToken cancellationToken);

    }
    public class WorkHistoryRepository(ICandidateAccountDataContext dataContext) : IWorkHistoryRepository
    {
        public async Task<WorkHistoryEntity> Insert(WorkHistoryEntity workHistoryEntity)
        {
            await dataContext.WorkExperienceEntities.AddAsync(workHistoryEntity);
            await dataContext.SaveChangesAsync();
            return workHistoryEntity;
        }

        public async Task Update(WorkHistoryEntity workHistoryEntity)
        {
            var entity = await dataContext.WorkExperienceEntities.SingleAsync(x => x.Id == workHistoryEntity.Id && x.ApplicationId == workHistoryEntity.ApplicationId);

            entity.WorkHistoryType = workHistoryEntity.WorkHistoryType;
            entity.StartDate = workHistoryEntity.StartDate;
            entity.EndDate = workHistoryEntity.EndDate;
            entity.JobTitle = workHistoryEntity.JobTitle;
            entity.Description = workHistoryEntity.Description;
            entity.Employer = workHistoryEntity.Employer;

            await dataContext.SaveChangesAsync();
        }

        public async Task<List<WorkHistoryEntity>> GetAll(Guid applicationId, Guid candidateId, WorkHistoryType? workHistoryType, CancellationToken cancellationToken)
        {
            var query = from wrk in dataContext.WorkExperienceEntities
                    .Where(fil => fil.ApplicationId == applicationId)
                    .Where(fil => workHistoryType == null || fil.WorkHistoryType == (byte) workHistoryType)
                    .OrderBy(a => a.EndDate.HasValue)
                    .ThenByDescending(a => a.StartDate)
                    .ThenBy(a => a.JobTitle)
                        join application in dataContext.ApplicationEntities.Where(fil => fil.CandidateId == candidateId && fil.Id == applicationId)
                            on wrk.ApplicationId equals application.Id
                        select wrk;

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<WorkHistoryEntity?> Get(Guid applicationId, Guid candidateId, Guid id, WorkHistoryType? workHistoryType, CancellationToken cancellationToken)
        {
            var query = from wrk in dataContext.WorkExperienceEntities
                    .Where(fil => fil.ApplicationId == applicationId)
                    .Where(fil => fil.Id == id)
                    .Where(fil => workHistoryType == null || fil.WorkHistoryType == (byte)workHistoryType)
                    .OrderBy(a => a.StartDate)
                    .ThenBy(a => a.JobTitle)
                join application in dataContext.ApplicationEntities.Where(fil => fil.CandidateId == candidateId && fil.Id == applicationId)
                    on wrk.ApplicationId equals application.Id
                select wrk;

            return await query.SingleOrDefaultAsync(cancellationToken);
        }
    }
}
