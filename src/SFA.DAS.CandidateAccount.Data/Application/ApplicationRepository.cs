using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Application;

public interface IApplicationRepository
{
    Task<Tuple<ApplicationEntity,bool>> Upsert(ApplicationEntity applicationEntity);
}

public class ApplicationRepository(ICandidateAccountDataContext dataContext) : IApplicationRepository
{
    public async Task<Tuple<ApplicationEntity,bool>> Upsert(ApplicationEntity applicationEntity)
    {
        var application = await dataContext.ApplicationEntities.SingleOrDefaultAsync(c =>
            c.VacancyReference == applicationEntity.VacancyReference &&
            c.CandidateId == applicationEntity.CandidateId);

        if (application == null)
        {
            await dataContext.ApplicationEntities.AddAsync(applicationEntity);
            await dataContext.SaveChangesAsync();
            return new Tuple<ApplicationEntity, bool>(applicationEntity, true);    
        }
        
        applicationEntity.UpdatedDate = DateTime.UtcNow;
        applicationEntity.Status = applicationEntity.Status;
        
        await dataContext.SaveChangesAsync();
        
        return new Tuple<ApplicationEntity, bool>(applicationEntity, false);
    }
}