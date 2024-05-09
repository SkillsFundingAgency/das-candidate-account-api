using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public class GetApplicationApiResponse
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
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public string? DisabilityStatus { get; set; }
    public required string VacancyReference { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public DateTime? WithdrawnDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? WithdrawnDate { get; set; }
    public List<AdditionalQuestion>? AdditionalQuestions { get; set; } = [];
    public AboutYouItem AboutYou { get; set; }
    public List<Qualification> Qualifications { get; set; }
    public List<WorkHistoryItem> WorkHistory { get; set; }
    public List<TrainingCourseItem> TrainingCourses { get; set; }
    public Candidate Candidate { get; set; }
    public string? ResponseNotes { get; set; }

    public DateTime? ResponseDate { get; set; }
    
    public static implicit operator GetApplicationApiResponse(Domain.Application.Application application)
    {
        
        return new GetApplicationApiResponse
        {
            Status = application.Status,
            TrainingCoursesStatus = application.TrainingCoursesStatus,
            WorkExperienceStatus = application.WorkExperienceStatus,
            QualificationsStatus = application.QualificationsStatus,
            JobsStatus = application.JobsStatus,
            DisabilityConfidenceStatus = application.DisabilityConfidenceStatus,
            SkillsAndStrengthStatus = application.SkillsAndStrengthStatus,
            InterviewAdjustmentsStatus = application.InterviewAdjustmentsStatus,
            AdditionalQuestion1Status = application.AdditionalQuestion1Status,
            AdditionalQuestion2Status = application.AdditionalQuestion2Status,
            InterestsStatus = application.InterestsStatus,
            EducationHistorySectionStatus = application.EducationHistorySectionStatus,
            WorkHistorySectionStatus = application.WorkHistorySectionStatus,
            ApplicationQuestionsSectionStatus = application.ApplicationQuestionsSectionStatus,
            InterviewAdjustmentsSectionStatus = application.InterviewAdjustmentsSectionStatus,
            DisabilityConfidenceSectionStatus = application.DisabilityConfidenceSectionStatus,
            ApplicationAllSectionStatus = application.ApplicationAllSectionStatus,
            WhatIsYourInterest = application.WhatIsYourInterest,
            ApplyUnderDisabilityConfidentScheme = application.ApplyUnderDisabilityConfidentScheme,
            Id = application.Id,
            CandidateId = application.CandidateId,
            DisabilityStatus = application.DisabilityStatus,
            VacancyReference = application.VacancyReference,
            AdditionalQuestions = application.AdditionalQuestions,
            SubmittedDate = application.SubmittedDate,
            WithdrawnDate = application.WithdrawnDate,
            CreatedDate = application.CreatedDate,
            WithdrawnDate = application.WithdrawnDate,
            ResponseDate = application.ResponseDate,
            ResponseNotes = application.ResponseNotes
        };
    }



    public static implicit operator GetApplicationApiResponse(ApplicationDetail applicationDetail)
    {
        
        return new GetApplicationApiResponse
        {
            Status = applicationDetail.Status,
            TrainingCoursesStatus = applicationDetail.TrainingCoursesStatus,
            WorkExperienceStatus = applicationDetail.WorkExperienceStatus,
            QualificationsStatus = applicationDetail.QualificationsStatus,
            JobsStatus = applicationDetail.JobsStatus,
            DisabilityConfidenceStatus = applicationDetail.DisabilityConfidenceStatus,
            SkillsAndStrengthStatus = applicationDetail.SkillsAndStrengthStatus,
            InterviewAdjustmentsStatus = applicationDetail.InterviewAdjustmentsStatus,
            AdditionalQuestion1Status = applicationDetail.AdditionalQuestion1Status,
            AdditionalQuestion2Status = applicationDetail.AdditionalQuestion2Status,
            InterestsStatus = applicationDetail.InterestsStatus,
            EducationHistorySectionStatus = applicationDetail.EducationHistorySectionStatus,
            WorkHistorySectionStatus = applicationDetail.WorkHistorySectionStatus,
            ApplicationQuestionsSectionStatus = applicationDetail.ApplicationQuestionsSectionStatus,
            InterviewAdjustmentsSectionStatus = applicationDetail.InterviewAdjustmentsSectionStatus,
            DisabilityConfidenceSectionStatus = applicationDetail.DisabilityConfidenceSectionStatus,
            ApplicationAllSectionStatus = applicationDetail.ApplicationAllSectionStatus,
            WhatIsYourInterest = applicationDetail.WhatIsYourInterest,
            ApplyUnderDisabilityConfidentScheme = applicationDetail.ApplyUnderDisabilityConfidentScheme,
            Id = applicationDetail.Id,
            CandidateId = applicationDetail.CandidateId,
            DisabilityStatus = applicationDetail.DisabilityStatus,
            VacancyReference = applicationDetail.VacancyReference,
            TrainingCourses = applicationDetail.TrainingCourses.Select(c=>(TrainingCourseItem)c).ToList(),
            Qualifications = applicationDetail.Qualifications,
            AdditionalQuestions = applicationDetail.AdditionalQuestions,
            WorkHistory = applicationDetail.WorkHistory.Select(c=>(WorkHistoryItem)c).ToList(),
            AboutYou = applicationDetail.AboutYou,
            Candidate = applicationDetail.Candidate,
            SubmittedDate = applicationDetail.SubmittedDate,
            WithdrawnDate = applicationDetail.WithdrawnDate,
            CreatedDate = applicationDetail.CreatedDate,
            WithdrawnDate = applicationDetail.WithdrawnDate,
            ResponseDate = applicationDetail.ResponseDate,
            ResponseNotes = applicationDetail.ResponseNotes
        };
    }
}