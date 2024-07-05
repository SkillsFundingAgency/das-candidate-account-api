using MediatR;
using SFA.DAS.CandidateAccount.Data.Candidate;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByMigratedId;

public class GetCandidateByMigratedIdQueryHandler(ICandidateRepository candidateRepository) : IRequestHandler<GetCandidateByMigratedIdQuery, GetCandidateByMigratedIdQueryResult>
{
    public async Task<GetCandidateByMigratedIdQueryResult> Handle(GetCandidateByMigratedIdQuery request, CancellationToken cancellationToken)
    {
        var result = await candidateRepository.GetByMigratedCandidateId(request.MigratedCandidateId);

        if (result == null)
        {
            return new GetCandidateByMigratedIdQueryResult
            {
                Candidate = null
            };
        }

        return new GetCandidateByMigratedIdQueryResult
        {
            Candidate = result
        };
    }
}