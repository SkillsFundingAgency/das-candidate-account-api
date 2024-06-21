namespace SFA.DAS.CandidateAccount.Domain.Candidate;
public class PreferenceEntity
{
    public Guid PreferenceId { get; set; }
    public string PreferenceMeaning { get; set; }
    public string PreferenceHint { get; set; }
    public string PreferenceType { get; set; }
}