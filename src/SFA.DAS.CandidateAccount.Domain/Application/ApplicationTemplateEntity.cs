namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class ApplicationTemplateEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string DisabilityStatus { get; set; }
    }
}
