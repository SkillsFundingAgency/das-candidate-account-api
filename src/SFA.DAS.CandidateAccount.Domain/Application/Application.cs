namespace SFA.DAS.CandidateAccount.Domain.Application;

public class Application
{
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public string? DisabilityStatus { get; set; }
    public required string VacancyReference { get; set; }
    public ApplicationStatus Status { get; set; }
    public SectionStatus TrainingCourseStatus { get; set; }
    public SectionStatus WorkExperienceStatus { get; set; }
    public SectionStatus QualificationStatus { get; set; }
    public SectionStatus JobStatus { get; set; }
    public SectionStatus DisabilityConfidenceStatus { get; set; }
    public SectionStatus SkillsAndStrengthStatus { get; set; }

    public SectionStatus InterviewAdjustmentsStatus { get; set; }

    public SectionStatus HowWillYouTravelStatus { get; set; }

    public SectionStatus AdditionalQuestion2Status { get; set; }

    public SectionStatus AdditionalQuestion1Status { get; set; }

    public SectionStatus InterestsStatus { get; set; }
    public SectionStatus EducationHistorySectionStatus { get; set; }
    public SectionStatus WorkHistorySectionStatus { get; set; }
    public SectionStatus ApplicationQuestionsSectionStatus { get; set; }
    public SectionStatus InterviewAdjustmentsSectionStatus { get; set; }
    public SectionStatus DisabilityConfidenceSectionStatus { get; set; }

    public static implicit operator Application(ApplicationEntity source)
    {
        return new Application
        {
            Id = source.Id,
            CandidateId = source.CandidateId,
            DisabilityStatus = source.DisabilityStatus,
            VacancyReference = source.VacancyReference,
            Status = ParseValue<ApplicationStatus>(source.Status),
            DisabilityConfidenceStatus = ParseValue<SectionStatus>(source.DisabilityConfidenceStatus),
            JobStatus = ParseValue<SectionStatus>(source.JobsStatus),
            QualificationStatus = ParseValue<SectionStatus>(source.QualificationsStatus),
            WorkExperienceStatus = ParseValue<SectionStatus>(source.WorkExperienceStatus),
            TrainingCourseStatus = ParseValue<SectionStatus>(source.TrainingCoursesStatus),
            InterestsStatus = ParseValue<SectionStatus>(source.InterestsStatus),
            AdditionalQuestion1Status = ParseValue<SectionStatus>(source.AdditionalQuestion1Status),
            AdditionalQuestion2Status = ParseValue<SectionStatus>(source.AdditionalQuestion2Status),
            HowWillYouTravelStatus = ParseValue<SectionStatus>(source.HowWillYouTravelStatus),
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
                ParseValue<SectionStatus>(source.HowWillYouTravelStatus),
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
            ])
        };
    }

    private static SectionStatus GetSectionStatus(params SectionStatus[] sections)
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

        if (sectionStatus.All(c => c is SectionStatus.Completed or SectionStatus.NotRequired))
        {
            return SectionStatus.Completed;
        }
        if (sectionStatus.All(c => c is SectionStatus.NotStarted or SectionStatus.InProgress or SectionStatus.NotRequired))
        {
            return SectionStatus.InProgress;
        }
        
        return SectionStatus.NotStarted;
    }

    private static T ParseValue<T>(short status) where T : struct, Enum
    {
        Enum.TryParse<T>(status.ToString(), true, out var sectionStatus);
        return sectionStatus;
    }
}