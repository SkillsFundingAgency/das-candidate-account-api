using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsCount
{
    public class GetApplicationsCountQueryHandler(IApplicationRepository applicationRepository)
        : IRequestHandler<GetApplicationsCountQuery, GetApplicationsCountQueryResult>
    {
        public async Task<GetApplicationsCountQueryResult> Handle(GetApplicationsCountQuery query,
            CancellationToken cancellationToken)
        {
            var applications = await applicationRepository
                .GetCountByStatus(query.CandidateId, query.Statuses.Select(status => (short)status).ToList(), cancellationToken);

            return new GetApplicationsCountQueryResult
            {
                Stats = applications.GroupBy(app => app.Status).Select(app =>
                    new GetApplicationsCountQueryResult.ApplicationStats
                    {
                        Status = (ApplicationStatus)app.Key,
                        Count = app.Count()
                    }).ToList()
            };
        }
    }
}