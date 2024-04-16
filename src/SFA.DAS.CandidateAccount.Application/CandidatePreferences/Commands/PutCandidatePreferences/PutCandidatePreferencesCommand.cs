using MediatR;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Application.CandidatePreferences.Commands.PutCandidatePreferences;
public class PutCandidatePreferencesCommand : IRequest<PutCandidatePreferencesCommandResult>
{
    public List<CandidatePreference> CandidatePreferences { get; set; }
}
