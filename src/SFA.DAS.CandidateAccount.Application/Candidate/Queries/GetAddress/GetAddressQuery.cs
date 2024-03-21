using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetAddress;

public record GetAddressQuery : IRequest<GetAddressQueryResult>
{
    public required Guid CandidateId { get; init; }
}