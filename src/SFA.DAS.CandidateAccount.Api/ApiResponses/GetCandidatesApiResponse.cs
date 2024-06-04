using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public class GetCandidatesApiResponse
{
    public List<CandidateApplication> Candidates { get; set; }
}

public class CandidateApplication
{
    public Guid ApplicationId { get; set; }
    public DateTime ApplicationCreatedDate { get; set; }
    public Candidate Candidate { get; set; }
}