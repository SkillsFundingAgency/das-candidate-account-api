using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByMigratedId;

public class GetCandidateByMigratedIdQuery : IRequest<GetCandidateByMigratedIdQueryResult>
{
    public Guid MigratedCandidateId { get; set; }
}