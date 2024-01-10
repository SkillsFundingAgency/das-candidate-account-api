using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Application;

public interface IApplicationRepository
{
    Task Insert(ApplicationEntity applicationEntity);
}

public class ApplicationRepository(ICandidateAccountDataContext dataContext) : IApplicationRepository
{
    public async Task Insert(ApplicationEntity applicationEntity)
    {
        await dataContext.ApplicationEntities.AddAsync(applicationEntity);

        await dataContext.SaveChangesAsync();
    }
}