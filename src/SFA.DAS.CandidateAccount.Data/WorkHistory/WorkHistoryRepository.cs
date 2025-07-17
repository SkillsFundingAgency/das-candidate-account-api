using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.WorkExperience;

public interface IWorkHistoryRepository
{
    Task<Tuple<WorkHistoryEntity, bool>> UpsertWorkHistory(WorkHistory workHistoryEntity, Guid candidateId);
    Task<WorkHistoryEntity?> Get(Guid applicationId, Guid candidateId, Guid id, WorkHistoryType? workHistoryType, CancellationToken cancellationToken);
    Task<List<WorkHistoryEntity>> GetAll(Guid applicationId, Guid candidateId, WorkHistoryType? workHistoryType, CancellationToken cancellationToken);
    Task Delete(Guid applicationId, Guid id, Guid candidateId);
    Task DeleteAllAsync(Guid applicationId, Guid candidateId, CancellationToken cancellationToken);
}
public class WorkHistoryRepository(ICandidateAccountDataContext dataContext) : IWorkHistoryRepository
{
    public static readonly int MaximumItems = 100;

    public async Task<List<WorkHistoryEntity>> GetAll(Guid applicationId, Guid candidateId, WorkHistoryType? workHistoryType, CancellationToken cancellationToken)
    {
        var query = from wrk in dataContext.WorkExperienceEntities
                .Where(fil => fil.ApplicationId == applicationId)
                .Where(fil => workHistoryType == null || fil.WorkHistoryType == (byte)workHistoryType)
                .OrderBy(a => a.EndDate.HasValue)
                .ThenByDescending(a => a.StartDate)
                .ThenBy(a => a.JobTitle)
                .ThenBy(a => a.Employer)
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
            var itemCount = await dataContext.WorkExperienceEntities
                .Where(fil => fil.ApplicationId == workHistoryEntity.ApplicationId)
                .Where(fil => fil.WorkHistoryType == (byte)workHistoryEntity.WorkHistoryType)
                .CountAsync();

            if (itemCount >= MaximumItems)
            {
                throw new InvalidOperationException($"Cannot insert a new work history item for application {workHistoryEntity.ApplicationId}; maximum reached.");
            }

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
            .SingleOrDefaultAsync(w => w.Id == id && w.ApplicationId == applicationId && w.ApplicationEntity.CandidateId == candidateId);

        if (workHistory == null) 
        {  
            return; 
        }

        dataContext.WorkExperienceEntities.Remove(workHistory);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteAllAsync(Guid applicationId, Guid candidateId, CancellationToken cancellationToken)
    {
        var records = await dataContext
            .WorkExperienceEntities
            .Where(x => x.ApplicationId == applicationId && x.ApplicationEntity.CandidateId == candidateId)
            .ToListAsync(cancellationToken);

        if (records is not { Count: > 0 })
        {
            return;
        }

        dataContext.WorkExperienceEntities.RemoveRange(records);
        await dataContext.SaveChangesAsync(cancellationToken);
    }
}