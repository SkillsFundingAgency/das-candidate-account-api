using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public class GetCandidatesApiResponse
{
    public List<Candidate> Candidates { get; set; }
}