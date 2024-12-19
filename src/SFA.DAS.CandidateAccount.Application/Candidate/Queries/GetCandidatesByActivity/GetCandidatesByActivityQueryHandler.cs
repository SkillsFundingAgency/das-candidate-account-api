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
            var candidates = await repository.GetCandidatesByActivity(request.CutOffDateTime);

            return new GetCandidatesByActivityQueryResult
            {
                Candidates = candidates is {Count: > 0} 
                    ? candidates.Select(x => (Domain.Candidate.Candidate)x).ToList() 
                    : []
            };
        }
    }
}