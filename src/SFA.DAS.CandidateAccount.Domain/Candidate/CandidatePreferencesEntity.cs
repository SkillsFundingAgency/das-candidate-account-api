namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class CandidatePreferencesEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public Guid PreferenceId { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string ContactMethod { get; set; }
    }
}
