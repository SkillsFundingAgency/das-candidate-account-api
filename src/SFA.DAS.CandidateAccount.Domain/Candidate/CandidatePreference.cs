namespace SFA.DAS.CandidateAccount.Domain.Candidate;
public class CandidatePreference
{
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public Guid PreferenceId { get; set; }
    public bool? Status { get; set; }
    public string ContactMethod { get; set; }

    public static implicit operator CandidatePreference(CandidatePreferencesEntity source)
    {
        return new CandidatePreference
        {
            Id = source.Id,
            CandidateId = source.CandidateId,
            PreferenceId = source.PreferenceId,
            Status = source.Status,
            ContactMethod = source.ContactMethod
        };
    }
}
