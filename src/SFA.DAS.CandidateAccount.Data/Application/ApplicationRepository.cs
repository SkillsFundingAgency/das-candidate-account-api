using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Application;

public interface IApplicationRepository
{
    Task<Tuple<ApplicationEntity,bool>> Upsert(ApplicationEntity applicationEntity);
    Task<ApplicationEntity?> GetById(Guid applicationId);
    Task<ApplicationEntity> Update(ApplicationEntity application);
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
        
        application.UpdatedDate = DateTime.UtcNow;
        application.Status = applicationEntity.Status;
        application.QualificationsStatus = applicationEntity.QualificationsStatus != 0 ? applicationEntity.QualificationsStatus : application.QualificationsStatus;
        application.TrainingCoursesStatus = applicationEntity.TrainingCoursesStatus != 0 ? applicationEntity.TrainingCoursesStatus : application.TrainingCoursesStatus;
        application.JobsStatus = applicationEntity.JobsStatus != 0 ? applicationEntity.JobsStatus : application.JobsStatus;
        application.WorkExperienceStatus = applicationEntity.WorkExperienceStatus != 0 ? applicationEntity.WorkExperienceStatus : application.WorkExperienceStatus;
        application.SkillsAndStrengthStatus = applicationEntity.SkillsAndStrengthStatus != 0 ? applicationEntity.SkillsAndStrengthStatus : application.SkillsAndStrengthStatus;
        application.InterestsStatus = applicationEntity.InterestsStatus != 0 ? applicationEntity.InterestsStatus : application.InterestsStatus;
        application.AdditionalQuestion1Status = applicationEntity.AdditionalQuestion1Status != 0 ? applicationEntity.AdditionalQuestion1Status : application.AdditionalQuestion1Status;
        application.AdditionalQuestion2Status = applicationEntity.AdditionalQuestion2Status != 0 ? applicationEntity.AdditionalQuestion2Status : application.AdditionalQuestion2Status;
        application.InterviewAdjustmentsStatus = applicationEntity.InterviewAdjustmentsStatus != 0 ? applicationEntity.InterviewAdjustmentsStatus : application.InterviewAdjustmentsStatus;
        application.DisabilityConfidenceStatus = applicationEntity.DisabilityConfidenceStatus != 0 ? applicationEntity.DisabilityConfidenceStatus : application.DisabilityConfidenceStatus;
        
        await dataContext.SaveChangesAsync();
        
        return new Tuple<ApplicationEntity, bool>(application, false);
    }

    public async Task<ApplicationEntity?> GetById(Guid applicationId)
    {
        try
        {
            return await dataContext.ApplicationEntities.FindAsync(applicationId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ApplicationEntity> Update(ApplicationEntity application)
    {
        dataContext.ApplicationEntities.Update(application);
        await dataContext.SaveChangesAsync();
        return application;
    }
}