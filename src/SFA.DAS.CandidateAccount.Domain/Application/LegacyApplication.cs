namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class LegacyApplication
    {
        public required Guid CandidateId { get; set; }
        public required string VacancyReference { get; set; }

        public ApplicationStatus Status { get; set; }
        public string SkillsAndStrengths { get; set; }
        public string Support { get; set; }
        public DateTime? DateApplied { get; set; }
        public DateTime? SuccessfulDateTime { get; set; }
        public DateTime? UnsuccessfulDateTime { get; set; }
        public string? AdditionalQuestion1 { get; set; }
        public string? AdditionalQuestion2 { get; set; }
        public string? AdditionalQuestion1Answer { get; set; }
        public string? AdditionalQuestion2Answer { get; set; }
        public bool IsDisabilityConfident { get; set; }
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
