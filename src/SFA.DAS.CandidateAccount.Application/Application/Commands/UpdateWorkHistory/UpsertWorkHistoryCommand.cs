using MediatR;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateWorkHistory
{
    public class UpsertWorkHistoryCommand : IRequest<UpsertWorkHistoryCommandResponse>
    {
        public required WorkHistory WorkHistory { get; set; }
        public required Guid CandidateId { get; set; }
    }
}
