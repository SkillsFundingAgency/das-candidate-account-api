using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetPagedApplications;

public record GetPagedApplicationsQuery(
    DateTime? AddedAfter,
    int PageNumber,
    int PageSize) : IRequest<GetPagedApplicationsQueryResult>;
