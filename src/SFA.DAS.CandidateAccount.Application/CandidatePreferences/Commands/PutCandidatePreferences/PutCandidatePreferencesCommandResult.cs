using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Application.CandidatePreferences.Commands.PutCandidatePreferences;
public class PutCandidatePreferencesCommandResult
{
    public List<CandidatePreferenceCommandResult> CandidatePreferences { get; set; } = new List<CandidatePreferenceCommandResult>();

    public class CandidatePreferenceCommandResult
    {
        public CandidatePreference CandidatePreference { get; set; }
        public bool Created { get; set; }
    }

    public static implicit operator PutCandidatePreferencesCommandResult(List<Tuple<bool, CandidatePreferencesEntity>> candidatePreferences)
    {
        return new PutCandidatePreferencesCommandResult
        {
            CandidatePreferences = candidatePreferences.Select(x => new CandidatePreferenceCommandResult
            {
                CandidatePreference = (CandidatePreference)x.Item2,
                Created = x.Item1
            }).ToList()
        };
    }
}
