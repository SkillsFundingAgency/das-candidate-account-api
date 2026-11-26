using MediatR;
using SFA.DAS.CandidateAccount.Data.Candidate;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetPagedCandidates;

public class GetPagedCandidatesQueryHandler(ICandidateRepository repository)
    : IRequestHandler<GetPagedCandidatesQuery, GetPagedCandidatesQueryResult>
{
    public async Task<GetPagedCandidatesQueryResult> Handle(GetPagedCandidatesQuery request,
        CancellationToken cancellationToken)
    {
        return await repository.GetPagedCandidates(
            request.AddedSince,
            request.PageNumber,
            request.PageSize,
            cancellationToken);
    }
}
