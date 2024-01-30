namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class EducationEntity
    {
        public Guid Id { get; set; }
        public string Institution { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
