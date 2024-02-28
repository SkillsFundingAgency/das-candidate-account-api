using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteWorkHistory
{
    public record DeleteWorkHistoryCommand : IRequest<Unit>
    {
        public Guid JobId { get; init; }
        public Guid ApplicationId { get; init; }
        public Guid CandidateId { get; init; }
    }
}
