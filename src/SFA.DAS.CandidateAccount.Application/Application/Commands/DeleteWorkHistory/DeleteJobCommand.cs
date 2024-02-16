using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteWorkHistory
{
    public class DeleteJobCommand : IRequest<Unit>
    {
        public Guid JobId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid CandidateId { get; set; }
    }
}
