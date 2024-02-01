using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationWorkHistories;

public record GetApplicationWorkHistoriesQueryResult
{
    public IEnumerable<WorkHistoryItem> WorkHistories { get; init; } = [];

    public static implicit operator GetApplicationWorkHistoriesQueryResult(List<WorkHistoryEntity> source)
    {
        return new GetApplicationWorkHistoriesQueryResult
        {
            WorkHistories = source.Select(entity => (WorkHistoryItem) entity)
        };
    }

    public class WorkHistoryItem
    {
        public Guid Id { get; set; }
        public WorkHistoryType WorkHistoryType { get; set; }
        public string? Employer { get; set; }
        public string? JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid ApplicationId { get; set; }
        public string? Description { get; set; }

        public static implicit operator WorkHistoryItem(WorkHistoryEntity source)
        {
            return new WorkHistoryItem
            {
                Id = source.Id,
                WorkHistoryType = (WorkHistoryType)source.WorkHistoryType,
                Employer = source.Employer,
                JobTitle = source.JobTitle,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                ApplicationId = source.ApplicationId,
                Description = source.Description,
            };
        }
    }
}