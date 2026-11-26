using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetPagedApplications;

public class GetPagedApplicationsQueryHandler(IApplicationRepository repository)
    : IRequestHandler<GetPagedApplicationsQuery, GetPagedApplicationsQueryResult>
{
    public async Task<GetPagedApplicationsQueryResult> Handle(GetPagedApplicationsQuery request,
        CancellationToken cancellationToken)
    {
        return await repository.GetPagedApplications(
            request.AddedAfter,
            request.PageNumber,
            request.PageSize,
            cancellationToken);
    }
}
