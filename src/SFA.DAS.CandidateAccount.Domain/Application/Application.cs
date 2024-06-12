using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Domain.Application;

public abstract class ApplicationBase
{
    public ApplicationStatus Status { get; set; }
    public SectionStatus TrainingCoursesStatus { get; set; }
    public SectionStatus WorkExperienceStatus { get; set; }
    public SectionStatus QualificationsStatus { get; set; }
    public SectionStatus JobsStatus { get; set; }
    public SectionStatus DisabilityConfidenceStatus { get; set; }
    public SectionStatus SkillsAndStrengthStatus { get; set; }
    public SectionStatus InterviewAdjustmentsStatus { get; set; }
    public SectionStatus AdditionalQuestion2Status { get; set; }
    public SectionStatus AdditionalQuestion1Status { get; set; }
    public SectionStatus InterestsStatus { get; set; }
    public SectionStatus EducationHistorySectionStatus { get; set; }
    public SectionStatus WorkHistorySectionStatus { get; set; }
    public SectionStatus ApplicationQuestionsSectionStatus { get; set; }
    public SectionStatus InterviewAdjustmentsSectionStatus { get; set; }
    public SectionStatus DisabilityConfidenceSectionStatus { get; set; }
    public SectionStatus ApplicationAllSectionStatus { get; set; }
    public string? WhatIsYourInterest { get; set; }
    public bool? ApplyUnderDisabilityConfidentScheme { get; set; }
    public DateTime? ResponseDate { get; set; }
    public string? ResponseNotes { get; set; }
    public string? Strengths { get; set; }
    public string? Support { get; set; }


    protected static T ParseValue<T>(short status) where T : struct, Enum
    {
        Enum.TryParse<T>(status.ToString(), true, out var sectionStatus);
        return sectionStatus;
    }
    protected static SectionStatus GetSectionStatus(params SectionStatus[] sections)
    {
        var sectionStatus = sections.ToList();
        if (sectionStatus.TrueForAll(c => c == SectionStatus.NotStarted))
        {
            return SectionStatus.NotStarted;
        }
        if (sectionStatus.TrueForAll(c => c == SectionStatus.InProgress))
        {
            return SectionStatus.InProgress;
        }

        if (sectionStatus.All(c => c is SectionStatus.Completed or SectionStatus.NotRequired or SectionStatus.PreviousAnswer))
        {
            return SectionStatus.Completed;
        }
        if (sectionStatus.All(c => c is SectionStatus.NotStarted or SectionStatus.InProgress or SectionStatus.NotRequired))
        {
            return SectionStatus.InProgress;
        }
        
        return SectionStatus.NotStarted;
    }
}

public class ApplicationDetail : Application
{
    public List<Qualification> Qualifications { get; set; }
    public List<WorkHistory> WorkHistory { get; set; }
    public List<TrainingCourse> TrainingCourses { get; set; }
    public Candidate.Candidate Candidate { get; set; }
    public static implicit operator ApplicationDetail(ApplicationEntity source)
    {
        return new ApplicationDetail
        {
            Id = source.Id,
            CandidateId = source.CandidateId,
            DisabilityStatus = source.DisabilityStatus,
            VacancyReference = source.VacancyReference,
            Status = ParseValue<ApplicationStatus>(source.Status),
            DisabilityConfidenceStatus = ParseValue<SectionStatus>(source.DisabilityConfidenceStatus),
            JobsStatus = ParseValue<SectionStatus>(source.JobsStatus),
            QualificationsStatus = ParseValue<SectionStatus>(source.QualificationsStatus),
            WorkExperienceStatus = ParseValue<SectionStatus>(source.WorkExperienceStatus),
            TrainingCoursesStatus = ParseValue<SectionStatus>(source.TrainingCoursesStatus),
            InterestsStatus = ParseValue<SectionStatus>(source.InterestsStatus),
            AdditionalQuestion1Status = ParseValue<SectionStatus>(source.AdditionalQuestion1Status),
            AdditionalQuestion2Status = ParseValue<SectionStatus>(source.AdditionalQuestion2Status),
            InterviewAdjustmentsStatus = ParseValue<SectionStatus>(source.InterviewAdjustmentsStatus),
            SkillsAndStrengthStatus = ParseValue<SectionStatus>(source.SkillsAndStrengthStatus),
            EducationHistorySectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.QualificationsStatus),
                ParseValue<SectionStatus>(source.TrainingCoursesStatus)
            ]),
            WorkHistorySectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.JobsStatus),
                ParseValue<SectionStatus>(source.WorkExperienceStatus)
            ]),
            ApplicationQuestionsSectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.SkillsAndStrengthStatus),
                ParseValue<SectionStatus>(source.InterestsStatus),
                ParseValue<SectionStatus>(source.AdditionalQuestion1Status),
                ParseValue<SectionStatus>(source.AdditionalQuestion2Status)
            ]),
            InterviewAdjustmentsSectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.InterviewAdjustmentsStatus)
            ]),
            DisabilityConfidenceSectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.DisabilityConfidenceStatus)
            ]),
            ApplicationAllSectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.QualificationsStatus),
                ParseValue<SectionStatus>(source.TrainingCoursesStatus),
                ParseValue<SectionStatus>(source.JobsStatus),
                ParseValue<SectionStatus>(source.WorkExperienceStatus),
                ParseValue<SectionStatus>(source.SkillsAndStrengthStatus),
                ParseValue<SectionStatus>(source.InterestsStatus),
                ParseValue<SectionStatus>(source.AdditionalQuestion1Status),
                ParseValue<SectionStatus>(source.AdditionalQuestion2Status),
                ParseValue<SectionStatus>(source.InterviewAdjustmentsStatus),
                ParseValue<SectionStatus>(source.DisabilityConfidenceStatus)
            ]),
            WhatIsYourInterest = source.WhatIsYourInterest,
            ApplyUnderDisabilityConfidentScheme = source.ApplyUnderDisabilityConfidentScheme,
            AdditionalQuestions = source.AdditionalQuestionEntities?.Select(c=>(AdditionalQuestion)c).ToList(),
            Candidate = source.CandidateEntity,
            Qualifications = source.QualificationEntities.Select(c=>(Qualification)c).ToList(),
            WorkHistory = source.WorkHistoryEntities.Select(c=>(WorkHistory)c).ToList(),
            TrainingCourses = source.TrainingCourseEntities.Select(c=>(TrainingCourse)c).ToList(),
            SubmittedDate = source.SubmittedDate,
            WithdrawnDate = source.WithdrawnDate,
            CreatedDate = source.CreatedDate,
            ResponseNotes = source.ResponseNotes,
            ResponseDate = source.ResponseDate,
            PreviousAnswersSourceId = source.PreviousAnswersSourceId,
            Strengths = source.Strengths,
            Support = source.Support,
            MigrationDate = source.MigrationDate
        };
    }
}

public class Application : ApplicationBase
{
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public DateTime? WithdrawnDate { get; set; }
    public string? DisabilityStatus { get; set; }
    public required string VacancyReference { get; set; }
    public List<AdditionalQuestion>? AdditionalQuestions { get; set; } = [];
    public Guid? PreviousAnswersSourceId { get; set; }
    public string? Strengths { get; set; }
    public string? Support { get; set; }
public DateTime? MigrationDate { get; set; }


    public static implicit operator Application(ApplicationEntity source)
    {
        return new Application
        {
            Id = source.Id,
            CandidateId = source.CandidateId,
            CreatedDate = source.CreatedDate,
            SubmittedDate = source.SubmittedDate,
            WithdrawnDate = source.WithdrawnDate,
            DisabilityStatus = source.DisabilityStatus,
            VacancyReference = source.VacancyReference,
            Status = ParseValue<ApplicationStatus>(source.Status),
            DisabilityConfidenceStatus = ParseValue<SectionStatus>(source.DisabilityConfidenceStatus),
            JobsStatus = ParseValue<SectionStatus>(source.JobsStatus),
            QualificationsStatus = ParseValue<SectionStatus>(source.QualificationsStatus),
            WorkExperienceStatus = ParseValue<SectionStatus>(source.WorkExperienceStatus),
            TrainingCoursesStatus = ParseValue<SectionStatus>(source.TrainingCoursesStatus),
            InterestsStatus = ParseValue<SectionStatus>(source.InterestsStatus),
            AdditionalQuestion1Status = ParseValue<SectionStatus>(source.AdditionalQuestion1Status),
            AdditionalQuestion2Status = ParseValue<SectionStatus>(source.AdditionalQuestion2Status),
            InterviewAdjustmentsStatus = ParseValue<SectionStatus>(source.InterviewAdjustmentsStatus),
            SkillsAndStrengthStatus = ParseValue<SectionStatus>(source.SkillsAndStrengthStatus),
            EducationHistorySectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.QualificationsStatus),
                ParseValue<SectionStatus>(source.TrainingCoursesStatus)
            ]),
            WorkHistorySectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.JobsStatus),
                ParseValue<SectionStatus>(source.WorkExperienceStatus)
            ]),
            ApplicationQuestionsSectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.SkillsAndStrengthStatus),
                ParseValue<SectionStatus>(source.InterestsStatus),
                ParseValue<SectionStatus>(source.AdditionalQuestion1Status),
                ParseValue<SectionStatus>(source.AdditionalQuestion2Status)
            ]),
            InterviewAdjustmentsSectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.InterviewAdjustmentsStatus)
            ]),
            DisabilityConfidenceSectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.DisabilityConfidenceStatus)
            ]),
            ApplicationAllSectionStatus = GetSectionStatus(
            [
                ParseValue<SectionStatus>(source.QualificationsStatus),
                ParseValue<SectionStatus>(source.TrainingCoursesStatus),
                ParseValue<SectionStatus>(source.JobsStatus),
                ParseValue<SectionStatus>(source.WorkExperienceStatus),
                ParseValue<SectionStatus>(source.SkillsAndStrengthStatus),
                ParseValue<SectionStatus>(source.InterestsStatus),
                ParseValue<SectionStatus>(source.AdditionalQuestion1Status),
                ParseValue<SectionStatus>(source.AdditionalQuestion2Status),
                ParseValue<SectionStatus>(source.InterviewAdjustmentsStatus),
                ParseValue<SectionStatus>(source.DisabilityConfidenceStatus)
            ]),
            WhatIsYourInterest = source.WhatIsYourInterest,
            ApplyUnderDisabilityConfidentScheme = source.ApplyUnderDisabilityConfidentScheme,
            AdditionalQuestions = source.AdditionalQuestionEntities?.Select(c=>(AdditionalQuestion)c).ToList()!,
            ResponseNotes = source.ResponseNotes,
            ResponseDate = source.ResponseDate,
            PreviousAnswersSourceId = source.PreviousAnswersSourceId,
            Strengths = source.Strengths,
            Support = source.Support,
            MigrationDate = source.MigrationDate
        };
    }
}