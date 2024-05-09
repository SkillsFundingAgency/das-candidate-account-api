using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class ApplicationEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string? DisabilityStatus { get; set; }
        public string VacancyReference { get; set; } = null!;
        public short Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? WithdrawnDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string? ResponseNotes { get; set; }
        public virtual CandidateEntity CandidateEntity { get; set; } = null!;
        public short QualificationsStatus { get; set; }
        public short TrainingCoursesStatus { get; set; }
        public short JobsStatus { get; set; }
        public short WorkExperienceStatus { get; set; }
        public short DisabilityConfidenceStatus { get; set; }
        public short SkillsAndStrengthStatus { get; set; }
        public short InterestsStatus { get; set; }
        public short AdditionalQuestion1Status { get; set; }
        public short AdditionalQuestion2Status { get; set; }
        public short InterviewAdjustmentsStatus { get; set; }
        public string? WhatIsYourInterest { get; set; }
        public bool? ApplyUnderDisabilityConfidentScheme { get; set; }
        public DateTime? WithdrawnDate { get; set; }
        public virtual ICollection<WorkHistoryEntity> WorkHistoryEntities { get; set; } = null!;
        public virtual ICollection<TrainingCourseEntity> TrainingCourseEntities { get; set; } = null!;
        public virtual ICollection<QualificationEntity> QualificationEntities { get; set; } = null!;
        public virtual ICollection<AdditionalQuestionEntity>? AdditionalQuestionEntities { get; set; } = null!;
        public virtual AboutYouEntity? AboutYouEntity { get; set; } = null;
        
    }
}
