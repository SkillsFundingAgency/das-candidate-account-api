using MediatR;
using SFA.DAS.CandidateAccount.Data.WorkExperience;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteWorkHistory
{
    public class DeleteJobCommandHandler(IWorkHistoryRepository workHistoryRepository) : IRequestHandler<DeleteJobCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteJobCommand command, CancellationToken cancellation)
        {
            await workHistoryRepository.Delete(command.ApplicationId, command.JobId, command.CandidateId);

            return Unit.Value;

        }
    }
}
