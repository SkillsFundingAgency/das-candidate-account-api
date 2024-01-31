using MediatR;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationWorkHistories;

public record GetApplicationWorkHistoriesQueryHandler(IWorkHistoryRepository WorkHistoryRepository) : IRequestHandler<GetApplicationWorkHistoriesQuery, GetApplicationWorkHistoriesQueryResult>
{
    public async Task<GetApplicationWorkHistoriesQueryResult> Handle(GetApplicationWorkHistoriesQuery request, CancellationToken cancellationToken)
    {
        var workHistories = await WorkHistoryRepository.Get(request.ApplicationId, request.CandidateId, cancellationToken);

        return new GetApplicationWorkHistoriesQueryResult
        {
            WorkHistories = workHistories.Select(wrk => (WorkHistory)wrk).ToList()
        };
    }
}