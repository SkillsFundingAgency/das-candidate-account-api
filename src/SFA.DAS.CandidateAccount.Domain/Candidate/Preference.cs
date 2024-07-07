namespace SFA.DAS.CandidateAccount.Domain.Candidate;

public class Preference
{
    public Guid PreferenceId { get; set; }
    public string PreferenceMeaning { get; set; }
    public string PreferenceHint { get; set; }
    public string PreferenceType { get; set; }
    
    public static implicit operator Preference(PreferenceEntity source)
    {
        return new Preference
        {
            PreferenceHint = source.PreferenceHint,
            PreferenceId = source.PreferenceId,
            PreferenceMeaning = source.PreferenceMeaning,
            PreferenceType = source.PreferenceType
        };
    }
}