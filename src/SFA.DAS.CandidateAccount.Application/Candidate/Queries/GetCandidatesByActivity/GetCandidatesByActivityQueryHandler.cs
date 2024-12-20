using MediatR;
using SFA.DAS.CandidateAccount.Data.Candidate;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByActivity
{
    public class GetCandidatesByActivityQueryHandler(ICandidateRepository repository)
        : IRequestHandler<GetCandidatesByActivityQuery, GetCandidatesByActivityQueryResult>
    {
        public async Task<GetCandidatesByActivityQueryResult> Handle(GetCandidatesByActivityQuery request,
            CancellationToken cancellationToken)
        {
            return await repository.GetCandidatesByActivity(
                request.CutOffDateTime,
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
    }
}