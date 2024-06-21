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
        public virtual CandidateEntity Candidate { get; set; }

        public static implicit operator CandidatePreferencesEntity(CandidatePreference source)
        {
            return new CandidatePreferencesEntity
            {
                Id = source.Id,
                CandidateId = source.CandidateId,
                PreferenceId = source.PreferenceId,
                Status = source.Status,
                ContactMethod = source.ContactMethod
            };
        }
    }
}
