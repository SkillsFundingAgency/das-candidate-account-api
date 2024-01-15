using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.ApplicationTemplate;

public interface IApplicationTemplateRepository
{
    Task<Tuple<ApplicationTemplateEntity,bool>> Upsert(ApplicationTemplateEntity applicationTemplateEntity);
}

public class ApplicationTemplateRepository(ICandidateAccountDataContext dataContext) : IApplicationTemplateRepository
{
    public async Task<Tuple<ApplicationTemplateEntity,bool>> Upsert(ApplicationTemplateEntity applicationTemplateEntity)
    {
        var applicationTemplate = await dataContext.ApplicationTemplateEntities.SingleOrDefaultAsync(c =>
            c.VacancyReference.Equals(applicationTemplateEntity.VacancyReference) &&
            c.CandidateId.Equals(applicationTemplateEntity.CandidateId));

        if (applicationTemplate == null)
        {
            await dataContext.ApplicationTemplateEntities.AddAsync(applicationTemplateEntity);
            await dataContext.SaveChangesAsync();
            return new Tuple<ApplicationTemplateEntity, bool>(applicationTemplateEntity, true);    
        }
        
        applicationTemplateEntity.UpdatedDate = DateTime.UtcNow;
        applicationTemplateEntity.Status = applicationTemplateEntity.Status;
        
        await dataContext.SaveChangesAsync();
        
        return new Tuple<ApplicationTemplateEntity, bool>(applicationTemplateEntity, false);
    }
}