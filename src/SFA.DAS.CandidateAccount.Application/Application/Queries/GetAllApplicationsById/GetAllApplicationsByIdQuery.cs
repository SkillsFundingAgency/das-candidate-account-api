using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetAllApplicationsById
{
    public sealed record GetAllApplicationsByIdQuery(List<Guid> ApplicationIds, bool IncludeDetails = false)
        : IRequest<GetAllApplicationsByIdQueryResult>;
}