using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationWorkHistories;

public record GetApplicationWorkHistoriesQuery : IRequest<GetApplicationWorkHistoriesQueryResult>
{
    public Guid ApplicationId { get; init; }
    public Guid CandidateId { get; set; }
}