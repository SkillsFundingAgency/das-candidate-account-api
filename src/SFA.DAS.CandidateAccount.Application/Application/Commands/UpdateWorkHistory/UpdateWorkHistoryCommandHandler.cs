using MediatR;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateWorkHistory;

public class UpdateWorkHistoryCommandHandler(IWorkHistoryRepository repository) : IRequestHandler<UpdateWorkHistoryCommand>
{
    public async Task Handle(UpdateWorkHistoryCommand request, CancellationToken cancellationToken)
    {
        await repository.Update(new WorkHistoryEntity
        {
            Id = request.Id,
            ApplicationId = request.ApplicationId,
            WorkHistoryType = (byte)request.WorkHistoryType,
            Description = request.JobDescription,
            Employer = request.EmployerName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            JobTitle = request.JobTitle
        });
    }
}