namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class WorkExperienceEntity
    {
        public Guid Id { get; set; }
        public string Employer { get; set; }
        public string JobTitle { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public Guid ApplicationTemplateId { get; set; }
        public string Description { get; set; }
    }
}
