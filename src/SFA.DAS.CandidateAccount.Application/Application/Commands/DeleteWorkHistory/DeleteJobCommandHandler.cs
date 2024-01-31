using MediatR;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteWorkHistory
{
    public class DeleteJobCommandHandler(
        IWorkHistoryRepository workHistoryRepository)
        : IRequestHandler<DeleteJobCommand, >
    {
        public async Task<> Handle(DeleteJobCommand command, CancellationToken cancellation)
        {
            var delete = await workHistoryRepository.Delete(command.ApplicationId, command.JobId, command.CandidateId);


        }
    }
}
