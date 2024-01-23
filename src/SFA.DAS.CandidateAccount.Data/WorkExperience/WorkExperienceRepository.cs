using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.WorkExperience
{
    public interface IWorkExperienceRepository
    {
        Task Insert(WorkExperienceEntity workExperienceEntity);

    }
    public class WorkExperienceRepository(ICandidateAccountDataContext dataContext) : IWorkExperienceRepository
    {
        public async Task Insert(WorkExperienceEntity workExperienceEntity)
        {
            await dataContext.WorkExperienceEntities.AddAsync(workExperienceEntity);
            await dataContext.SaveChangesAsync();
        }
    }
}
