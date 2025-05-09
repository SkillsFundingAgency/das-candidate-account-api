using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsCount
{
    public class GetApplicationsCountQueryHandler(IApplicationRepository applicationRepository)
        : IRequestHandler<GetApplicationsCountQuery, GetApplicationsCountQueryResult>
    {
        public async Task<GetApplicationsCountQueryResult> Handle(GetApplicationsCountQuery query,
            CancellationToken cancellationToken)
        {
            var applications = await applicationRepository
                .GetCountByStatus(query.CandidateId, (short)query.Status, cancellationToken);

            var applicationEntities = applications.ToList();
            if (applicationEntities.Count == 0)
            {
                return new GetApplicationsCountQueryResult
                {
                    ApplicationIds = [],
                    Status = query.Status,
                    Count = 0
                };
            }

            return new GetApplicationsCountQueryResult
            {
                ApplicationIds = applicationEntities.Select(a => a.Id).ToList(),
                Status = query.Status,
                Count = applicationEntities.Count
            };
        }
    }
}