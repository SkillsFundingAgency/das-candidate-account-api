using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.WorkExperience
{
    public interface IWorkHistoryRepository
    {
        Task<Tuple<WorkHistoryEntity, bool>> UpsertWorkHistory(WorkHistory workHistoryEntity, Guid candidateId);
        Task<WorkHistoryEntity> Insert(WorkHistoryEntity workHistoryEntity);
        Task Update(WorkHistoryEntity workHistoryEntity);
        Task<WorkHistoryEntity?> Get(Guid applicationId, Guid candidateId, Guid id, WorkHistoryType? workHistoryType, CancellationToken cancellationToken);
        Task<List<WorkHistoryEntity>> GetAll(Guid applicationId, Guid candidateId, WorkHistoryType? workHistoryType, CancellationToken cancellationToken);
        Task Delete(Guid applicationId, Guid id, Guid candidateId);
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
                    .Where(fil => workHistoryType == null || fil.WorkHistoryType == (byte)workHistoryType)
        Task Delete(Guid applicationId, Guid id, Guid candidateId);
        public async Task<Tuple<WorkHistoryEntity, bool>> UpsertWorkHistory(WorkHistory workHistoryEntity, Guid candidateId)
        {
            var query = from wrk in dataContext.WorkExperienceEntities
                    .Where(fil => fil.ApplicationId == workHistoryEntity.ApplicationId)
                    .Where(fil => fil.Id == workHistoryEntity.Id)
                        join application in dataContext.ApplicationEntities.Where(fil => fil.CandidateId == candidateId && fil.Id == workHistoryEntity.ApplicationId)
                        on wrk.ApplicationId equals application.Id
                        select wrk;

            var workHistory = await query.SingleOrDefaultAsync();

            if (workHistory == null)
            {
                await dataContext.WorkExperienceEntities.AddAsync((WorkHistoryEntity)workHistoryEntity);
                await dataContext.SaveChangesAsync();
                return new Tuple<WorkHistoryEntity, bool>(workHistoryEntity, true);
            }

            workHistory.WorkHistoryType = (byte)workHistoryEntity.WorkHistoryType;
            workHistory.StartDate = workHistoryEntity.StartDate;
            workHistory.EndDate = workHistoryEntity.EndDate;
            workHistory.JobTitle = workHistoryEntity.JobTitle;
            workHistory.Description = workHistoryEntity.Description;
            workHistory.Employer = workHistoryEntity.Employer;

            dataContext.WorkExperienceEntities.Update(workHistory);

            await dataContext.SaveChangesAsync();
            return new Tuple<WorkHistoryEntity, bool>(workHistory, false);
        }

        public async Task Delete(Guid applicationId, Guid id, Guid candidateId)
        {
            var workHistory = await dataContext.WorkExperienceEntities
            .Where(w => w.Id == id && w.ApplicationId == applicationId && w.ApplicationEntity.CandidateId == candidateId)
            .SingleOrDefaultAsync();

            dataContext.WorkExperienceEntities.Remove(workHistory);
            await dataContext.SaveChangesAsync();
        }
        }
    }
}



