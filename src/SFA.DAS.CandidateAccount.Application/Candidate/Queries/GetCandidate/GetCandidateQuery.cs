using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidate;

public class GetCandidateQuery : IRequest<GetCandidateQueryResult>
{
    public string Id { get; set; }
}