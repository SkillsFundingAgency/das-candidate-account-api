using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Application.ReferenceData.Queries.GetAvailablePreferences;

public class GetAvailablePreferencesQueryResult
{
    public List<Preference> Preferences { get; set; }
}