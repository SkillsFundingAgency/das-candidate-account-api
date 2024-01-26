using MediatR;
using SFA.DAS.CandidateAccount.Data.WorkExperience;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationWorkHistories;

public record GetApplicationWorkHistoriesQueryHandler(IWorkHistoryRepository WorkHistoryRepository) : IRequestHandler<GetApplicationWorkHistoriesQuery, GetApplicationWorkHistoriesQueryResult>
{
    public async Task<GetApplicationWorkHistoriesQueryResult> Handle(GetApplicationWorkHistoriesQuery request, CancellationToken cancellationToken)
    {
        var workHistoryEntities = await WorkHistoryRepository.Get(request.ApplicationId, cancellationToken);

        return new GetApplicationWorkHistoriesQueryResult
        {
            WorkHistories = workHistoryEntities
        };
    }
}