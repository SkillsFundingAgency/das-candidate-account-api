using MediatR;
using SFA.DAS.CandidateAccount.Data.WorkExperience;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteWorkHistory
{
    public class DeleteWorkHistoryCommandHandler(IWorkHistoryRepository workHistoryRepository) : IRequestHandler<DeleteWorkHistoryCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteWorkHistoryCommand command, CancellationToken cancellation)
        {
            await workHistoryRepository.Delete(command.ApplicationId, command.JobId, command.CandidateId);

            return Unit.Value;
        }
    }
}
