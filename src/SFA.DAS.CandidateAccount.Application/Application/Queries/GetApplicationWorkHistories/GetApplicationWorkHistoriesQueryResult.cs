namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationWorkHistories;

public record GetApplicationWorkHistoriesQueryResult
{
    public List<Domain.Application.WorkHistoryEntity> WorkHistories { get; init; } = [];
}