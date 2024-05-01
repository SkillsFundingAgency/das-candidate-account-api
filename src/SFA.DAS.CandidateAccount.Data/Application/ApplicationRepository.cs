using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;
using Exception = System.Exception;

namespace SFA.DAS.CandidateAccount.Data.Application;

public interface IApplicationRepository
{
    Task<bool> Exists(Guid candidateId, string vacancyReference);
    Task<Tuple<ApplicationEntity,bool>> Upsert(ApplicationEntity applicationEntity);
    Task<ApplicationEntity?> GetById(Guid applicationId);
    Task<ApplicationEntity> Update(ApplicationEntity application);
    Task<IEnumerable<ApplicationEntity>> GetByCandidateId(Guid candidateId, short? statusId);
    Task<ApplicationEntity> Clone(Guid applicationId, string vacancyReference, bool requiresDisabilityConfidence);
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
        return await dataContext.ApplicationEntities.FindAsync(applicationId);
    }

    public async Task<ApplicationEntity> Update(ApplicationEntity application)
    {
        dataContext.ApplicationEntities.Update(application);
        await dataContext.SaveChangesAsync();
        return application;
    }

    public async Task<bool> Exists(Guid candidateId, string vacancyReference)
    {
        var existing = await dataContext.ApplicationEntities
            .Where(x => x.CandidateId == candidateId && x.VacancyReference == vacancyReference)
            .FirstOrDefaultAsync();

        return existing != null;
    }

    public async Task<IEnumerable<ApplicationEntity>> GetByCandidateId(Guid candidateId, short? statusId)
    {
        return await dataContext.ApplicationEntities
            .Where(x => x.CandidateId == candidateId && (statusId == null || x.Status == statusId.Value))
            .ToListAsync();
    }

    public async Task<ApplicationEntity> Clone(Guid applicationId, string vacancyReference, bool requiresDisabilityConfidence)
    {
        try
        {
            var original = await dataContext.ApplicationEntities
                .Include(x => x.TrainingCourseEntities)
                .Include(x => x.QualificationEntities)
                .Include(x => x.WorkHistoryEntities)
                //.Include(x => x.AboutYouEntity)
                .AsNoTracking()
                .SingleAsync(x => x.Id == applicationId);

            original.Id = Guid.NewGuid();
            original.VacancyReference = vacancyReference;
            original.PreviousAnswersSourceId = applicationId;
            foreach (var tc in original.TrainingCourseEntities)
            {
                tc.Id = Guid.NewGuid();
            }

            foreach (var q in original.QualificationEntities)
            {
                q.Id = Guid.NewGuid();
            }

            foreach (var w in original.WorkHistoryEntities)
            {
                w.Id = Guid.NewGuid();
            }

            original.JobsStatus = (short)SectionStatus.PreviousAnswer;
            original.AdditionalQuestion1Status = (short)SectionStatus.PreviousAnswer;
            original.AdditionalQuestion2Status = (short)SectionStatus.PreviousAnswer;
            original.InterestsStatus = (short)SectionStatus.PreviousAnswer;
            original.QualificationsStatus = (short)SectionStatus.PreviousAnswer;
            original.WorkExperienceStatus = (short)SectionStatus.PreviousAnswer;
            original.SkillsAndStrengthStatus = (short)SectionStatus.PreviousAnswer;
            original.TrainingCoursesStatus = (short)SectionStatus.PreviousAnswer;
            original.InterviewAdjustmentsStatus = (short)SectionStatus.PreviousAnswer;
            //if(original.AboutYouEntity != null) original.AboutYouEntity.Id = Guid.NewGuid(); //todo: put this back in

            if (requiresDisabilityConfidence)
            {
                if(original.DisabilityConfidenceStatus == (short)SectionStatus.NotRequired)
                {
                    original.DisabilityConfidenceStatus = (short)SectionStatus.NotStarted;
                    original.ApplyUnderDisabilityConfidentScheme = null;
                }
                else
                {
                    original.DisabilityConfidenceStatus = (short)SectionStatus.PreviousAnswer;
                }
            }
            else
            {
                original.DisabilityConfidenceStatus = (short)SectionStatus.NotRequired;
            }

            dataContext.ApplicationEntities.Add(original);

            await dataContext.SaveChangesAsync();

            return original;

        }
        catch (Exception ex)
        {
            throw;
        }

    }

}