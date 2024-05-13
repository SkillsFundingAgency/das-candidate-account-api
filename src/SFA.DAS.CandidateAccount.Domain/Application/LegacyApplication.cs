namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class LegacyApplication
    {
        public required Guid CandidateId { get; set; }
        public required string VacancyReference { get; set; }

        public ApplicationStatus Status { get; set; }
        public string? DisabilityStatus { get; set; }
        public SectionStatus IsApplicationQuestionsComplete { get; set; }
        public SectionStatus IsDisabilityConfidenceComplete { get; set; }
        public SectionStatus IsEducationHistoryComplete { get; set; }
        public SectionStatus IsInterviewAdjustmentsComplete { get; set; }
        public SectionStatus IsWorkHistoryComplete { get; set; }
        public SectionStatus IsAdditionalQuestion1Complete { get; set; }
        public SectionStatus IsAdditionalQuestion2Complete { get; set; }
        public IEnumerable<string?> AdditionalQuestions { get; set; } = [];
        public List<Qualification> Qualifications { get; set; } = [];
        public List<TrainingCourse> TrainingCourses { get; set; } = [];
        public List<WorkExperienceItem> WorkExperience { get; set; } = [];

        public class Qualification
        {
            public string QualificationType { get; set; }
            public string Subject { get; set; }
            public string Grade { get; set; }
            public bool IsPredicted { get; set; }
            public int Year { get; set; }
        }
        public class WorkExperienceItem
        {
            public string Employer { get; set; }
            public string JobTitle { get; set; }
            public string Description { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
        }

        public class TrainingCourse
        {
            public string Provider { get; set; }
            public string Title { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
        }
    }
}
