using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Application;

public interface IApplicationRepository
{
    Task<Tuple<ApplicationEntity,bool>> Upsert(ApplicationEntity applicationEntity);
    Task<ApplicationEntity?> GetById(Guid applicationId, bool includeDetail = false);
    Task<ApplicationEntity> Update(ApplicationEntity application);
    Task<IEnumerable<ApplicationEntity>> GetByCandidateId(Guid candidateId, short? statusId);
    Task<ApplicationEntity?> GetByVacancyReference(Guid candidateId, string vacancyReference);
    Task<IEnumerable<ApplicationEntity>> GetApplicationsByVacancyReference(string vacancyReference, short? statusId = null, Guid? preferenceId = null, bool canEmailOnly = false);
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
        application.SubmittedDate = applicationEntity.Status == 1 ? DateTime.UtcNow : null;
        application.WithdrawnDate = applicationEntity.Status == 2 ? DateTime.UtcNow : null;
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

    public async Task<ApplicationEntity?> GetById(Guid applicationId, bool includeDetail)
    {
        var applicationEntity = !includeDetail ? 
            await dataContext.ApplicationEntities.Include(c=>c.AdditionalQuestionEntities).IgnoreAutoIncludes()
                .SingleOrDefaultAsync(c=>c.Id == applicationId) :
            await dataContext.ApplicationEntities
                .Include(c=>c.QualificationEntities)
                    .ThenInclude(c=>c.QualificationReferenceEntity)
                .Include(c=>c.TrainingCourseEntities)
                .Include(c=>c.WorkHistoryEntities)
                .Include(c=>c.AdditionalQuestionEntities)
                .Include(c=>c.CandidateEntity)
                    .ThenInclude(c=>c.Address)
                .Include(c=>c.AboutYouEntity)
                .IgnoreAutoIncludes()
                .SingleOrDefaultAsync(c=>c.Id == applicationId);
        return applicationEntity;
    }

    public async Task<ApplicationEntity> Update(ApplicationEntity application)
    {
        dataContext.ApplicationEntities.Update(application);
        await dataContext.SaveChangesAsync();
        return application;
    }

    public async Task<IEnumerable<ApplicationEntity>> GetByCandidateId(Guid candidateId, short? statusId)
    {
        return await dataContext.ApplicationEntities
            .Where(x => x.CandidateId == candidateId && (statusId == null || x.Status == statusId.Value))
            .ToListAsync();
    }

    public async Task<ApplicationEntity?> GetByVacancyReference(Guid candidateId, string vacancyReference)
    {
        var application = await dataContext.ApplicationEntities.SingleOrDefaultAsync(c =>
            c.VacancyReference == vacancyReference &&
            c.CandidateId == candidateId);

        return application ?? null;
    }

    public async Task<IEnumerable<ApplicationEntity>> GetApplicationsByVacancyReference(string vacancyReference, short? statusId = null, Guid? preferenceId = null, bool canEmailOnly = false)
    {
        return await dataContext.ApplicationEntities
            .Include(c => c.CandidateEntity)
                .ThenInclude(c => c.CandidatePreferences)
            .Include(c=>c.CandidateEntity)
                .ThenInclude(c=>c.Address)
            .Where(c => c.VacancyReference == vacancyReference && (statusId== null || c.Status == statusId) && c.CandidateEntity.CandidatePreferences.Count(x=>(preferenceId == null || x.PreferenceId == preferenceId) 
                && (!canEmailOnly || (x.ContactMethod == "email" && x.Status!.Value))) >= 1).ToListAsync();
    }
}