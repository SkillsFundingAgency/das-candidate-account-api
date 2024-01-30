using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.WorkExperience
{
    public interface IWorkHistoryRepository
    {
        Task<WorkHistoryEntity> Insert(WorkHistoryEntity workHistoryEntity);

    }
    public class WorkHistoryRepository(ICandidateAccountDataContext dataContext) : IWorkHistoryRepository
    {
        public async Task<WorkHistoryEntity> Insert(WorkHistoryEntity workHistoryEntity)
        {
            await dataContext.WorkExperienceEntities.AddAsync(workHistoryEntity);
            await dataContext.SaveChangesAsync();
            return workHistoryEntity;
        }
    }
}
