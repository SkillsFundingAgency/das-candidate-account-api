using MediatR;
using SFA.DAS.CandidateAccount.Data.Candidate;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByEmail;

public class GetCandidateByEmailQueryHandler(ICandidateRepository candidateRepository) : IRequestHandler<GetCandidateByEmailQuery, GetCandidateByEmailQueryResult>
{
    public async Task<GetCandidateByEmailQueryResult> Handle(GetCandidateByEmailQuery request, CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.GetCandidateByEmail(request.Email);

        return new GetCandidateByEmailQueryResult
        {
            Candidate = candidate
        };
    }
}