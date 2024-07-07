using MediatR;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetWorkHistoryItem
{
    public class GetWorkHistoryItemQuery : IRequest<GetWorkHistoryItemQueryResult?>
    {
        public Guid ApplicationId { get; init; }
        public Guid CandidateId { get; set; }
        public WorkHistoryType? WorkHistoryType { get; set; }
        public Guid Id { get; set; }
    }
}
