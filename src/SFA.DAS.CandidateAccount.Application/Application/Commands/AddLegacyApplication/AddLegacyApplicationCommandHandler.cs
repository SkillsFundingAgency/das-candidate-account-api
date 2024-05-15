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
            Status = (short)legacyApplication.Status,
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
            AdditionalQuestion1Status = legacyApplication.HasAdditionalQuestion1
                ? (short)SectionStatus.NotStarted
                : (short)SectionStatus.NotRequired,
            AdditionalQuestion2Status = legacyApplication.HasAdditionalQuestion2
                ? (short)SectionStatus.NotStarted
                : (short)SectionStatus.NotRequired,
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
            QualificationEntities = legacyApplication.Qualifications
                .Select(x => MapQualification(x, qualificationReferences)).ToList()
        };
    }

    private QualificationEntity MapQualification(LegacyApplication.Qualification source, List<QualificationReferenceEntity> qualificationReferences)
    {
        var result = new QualificationEntity
        {
            QualificationReferenceId = GetQualificationType(qualificationReferences, source.QualificationType),
            Subject = source.Subject,
            Grade = source.Grade
        };

        switch (source.QualificationType)
        {
            case "GCSE":
            case "AS Level":
            case "A Level":
            case "BTEC":
                result.AdditionalInformation = string.Empty;
                result.IsPredicted = source.IsPredicted;
                break;
            default:
                result.Subject = source.QualificationType;
                result.AdditionalInformation = source.Subject;
                break;
        }

        return result;
    }

    private Guid GetQualificationType(List<QualificationReferenceEntity> qualificationReferences, string source)
    {
        switch (source)
        {
            case "GCSE":
                return qualificationReferences.Single(x => string.Equals(x.Name, "GCSE", StringComparison.OrdinalIgnoreCase)).Id;
            case "AS Level":
                return qualificationReferences.Single(x => string.Equals(x.Name, "AS LEVEL", StringComparison.OrdinalIgnoreCase)).Id;
            case "A Level":
                return qualificationReferences.Single(x => string.Equals(x.Name, "A LEVEL", StringComparison.OrdinalIgnoreCase)).Id;
            case "BTEC":
                return qualificationReferences.Single(x => string.Equals(x.Name, "BTEC", StringComparison.OrdinalIgnoreCase)).Id;
            case "NVQ or SVQ Level 1":
            case "NVQ or SVQ Level 2":
            case "NVQ or SVQ Level 3":
            default:
                return qualificationReferences.Single(x => string.Equals(x.Name, "OTHER", StringComparison.OrdinalIgnoreCase)).Id;
        }
    }
}