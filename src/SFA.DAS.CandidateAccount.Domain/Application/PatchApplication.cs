namespace SFA.DAS.CandidateAccount.Domain.Application;

public class PatchApplication : ApplicationBase
{
    public static implicit operator PatchApplication(ApplicationEntity application)
    {
        return new PatchApplication
        {
            Status = ParseValue<ApplicationStatus>(application.Status),
            TrainingCoursesStatus = ParseValue<SectionStatus>(application.TrainingCoursesStatus),
            QualificationsStatus = ParseValue<SectionStatus>(application.QualificationsStatus),
            JobsStatus = ParseValue<SectionStatus>(application.JobsStatus),
            DisabilityConfidenceStatus = ParseValue<SectionStatus>(application.DisabilityConfidenceStatus),
            SkillsAndStrengthStatus = ParseValue<SectionStatus>(application.SkillsAndStrengthStatus),
            InterviewAdjustmentsStatus = ParseValue<SectionStatus>(application.InterviewAdjustmentsStatus),
            AdditionalQuestion1Status = ParseValue<SectionStatus>(application.AdditionalQuestion1Status),
            AdditionalQuestion2Status = ParseValue<SectionStatus>(application.AdditionalQuestion2Status),
            InterestsStatus = ParseValue<SectionStatus>(application.InterestsStatus),
            WorkExperienceStatus = ParseValue<SectionStatus>(application.WorkExperienceStatus),
            WhatIsYourInterest = application.WhatIsYourInterest,
            ApplyUnderDisabilityConfidentScheme = application.ApplyUnderDisabilityConfidentScheme,
            ResponseNotes = application.ResponseNotes,
            Strengths = application.Strengths,
            Support = application.Support
        };
    }
}