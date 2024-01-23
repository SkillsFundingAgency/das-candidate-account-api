namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class WorkHistoryEntity
    {
        public Guid Id { get; set; }
        public short WorkHistoryType { get; set; }
        public string Employer { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid ApplicationId { get; set; }
        public string Description { get; set; }
    }
}
