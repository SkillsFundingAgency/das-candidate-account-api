using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.ReferenceData;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.AddLegacyApplication;

public class AddLegacyApplicationCommandHandler(IApplicationRepository applicationRepository, IQualificationReferenceRepository qualificationReferenceRepository) : IRequestHandler<AddLegacyApplicationCommand, AddLegacyApplicationCommandResponse>
{
    public async Task<AddLegacyApplicationCommandResponse> Handle(AddLegacyApplicationCommand request, CancellationToken cancellationToken)
    {
        var entity = await CreateApplicationEntity(request.LegacyApplication);

        var result = await applicationRepository.Upsert(entity);

        return new AddLegacyApplicationCommandResponse
        {
            Id = result.Item1.Id
        };
    }

    private async Task<ApplicationEntity> CreateApplicationEntity(LegacyApplication legacyApplication)
    {
        var qualificationReferences = (await qualificationReferenceRepository.GetAll()).ToList();

        return new ApplicationEntity
        {
            CandidateId = legacyApplication.CandidateId,
            VacancyReference = legacyApplication.VacancyReference,
            Status = (short) legacyApplication.Status,
            AboutYouEntity = new AboutYouEntity
            {
                Strengths = legacyApplication.SkillsAndStrengths
            },
            //todo: check statuses required
            //DisabilityConfidenceStatus = (short)legacyApplication.IsDisabilityConfidenceComplete,
            SkillsAndStrengthStatus = (short)SectionStatus.Incomplete,
            JobsStatus = (short)SectionStatus.Incomplete,
            QualificationsStatus = (short)SectionStatus.Incomplete,
            TrainingCoursesStatus = (short)SectionStatus.Incomplete,
            //WorkExperienceStatus = (short)SectionStatus.Incomplete,
            AdditionalQuestion1Status = legacyApplication.HasAdditionalQuestion1 ? (short)SectionStatus.NotStarted : (short)SectionStatus.NotRequired,
            AdditionalQuestion2Status = legacyApplication.HasAdditionalQuestion2 ? (short)SectionStatus.NotStarted : (short)SectionStatus.NotRequired,
            //DisabilityStatus = legacyApplication.DisabilityStatus,
            TrainingCourseEntities = legacyApplication.TrainingCourses.Select(x => new TrainingCourseEntity
            {
                Title = x.Title,
                FromYear = x.FromDate == DateTime.MinValue ? null : x.FromDate.Year,
                ToYear = x.ToDate == DateTime.MinValue ? 0 : x.ToDate.Year, //todo: what should zero be?
                Provider = x.Provider
            }).ToList(),
            WorkHistoryEntities = legacyApplication.WorkExperience.Select(x => new WorkHistoryEntity
            {
                Employer = x.Employer,
                JobTitle = x.JobTitle,
                Description = x.Description,
                StartDate = x.FromDate == DateTime.MinValue ? DateTime.UtcNow : x.FromDate, //todo: what should zero be?
                EndDate = x.ToDate == DateTime.MinValue ? null : x.ToDate
            }).ToList(),
            QualificationEntities = legacyApplication.Qualifications.Select(x => new QualificationEntity
            {
                AdditionalInformation = string.Empty,
                Grade = x.Grade,
                IsPredicted = x.IsPredicted,
                Subject = x.Subject,
                //ToYear = x.Year,
                //todo: check/improve this lookup
                QualificationReferenceId = GetQualificationType(qualificationReferences, x.QualificationType)
            }).ToList()

        };
    }

    private Guid GetQualificationType(List<QualificationReferenceEntity> qualificationReferences, string source)
    {
        var qualificationReference = qualificationReferences.SingleOrDefault(y => y.Name == source);

        if (qualificationReference == null)
        {
            return qualificationReferences.SingleOrDefault(y => y.Name == "Other").Id;
        }

        return qualificationReference.Id;
    }
}