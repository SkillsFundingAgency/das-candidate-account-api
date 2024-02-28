using MediatR;
using SFA.DAS.CandidateAccount.Data.WorkExperience;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteWorkHistory
{
    public record DeleteWorkHistoryCommandHandler(IWorkHistoryRepository WorkHistoryRepository) : IRequestHandler<DeleteWorkHistoryCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteWorkHistoryCommand command, CancellationToken cancellationToken)
        {
            await WorkHistoryRepository.Delete(command.ApplicationId, command.JobId, command.CandidateId);

            return Unit.Value;
        }
    }
}
