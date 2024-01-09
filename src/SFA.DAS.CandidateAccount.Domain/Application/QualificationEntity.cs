namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class QualificationEntity
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
        public int ToYear { get; set; }
        public bool IsPredicted { get; set; }
        public Guid ApplicationTemplateId { get; set; }
    }
}
