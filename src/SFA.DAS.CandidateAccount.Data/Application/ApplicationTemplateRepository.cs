using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Application;

public interface IApplicationTemplateRepository
{
    Task Insert(ApplicationTemplateEntity applicationTemplateEntity);
}

public class ApplicationTemplateRepository(ICandidateAccountDataContext dataContext) : IApplicationTemplateRepository
{
    public async Task Insert(ApplicationTemplateEntity applicationTemplateEntity)
    {
        await dataContext.ApplicationTemplateEntities.AddAsync(applicationTemplateEntity);

        await dataContext.SaveChangesAsync();
    }
}