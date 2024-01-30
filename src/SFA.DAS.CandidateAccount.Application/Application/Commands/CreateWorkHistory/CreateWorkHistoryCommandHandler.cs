using MediatR;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.CreateWorkHistory;

public class CreateWorkHistoryCommandHandler(IWorkHistoryRepository workHistoryRepository) :
    IRequestHandler<CreateWorkHistoryCommand, CreateWorkHistoryResponse>
{
    public async Task<CreateWorkHistoryResponse> Handle(CreateWorkHistoryCommand request, CancellationToken cancellationToken)
    {
        var result = await workHistoryRepository.Insert(new WorkHistoryEntity
        {
            WorkHistoryType = (short) request.WorkHistoryType,
            ApplicationId = request.ApplicationId,
            Description = request.JobDescription,
            Employer = request.EmployerName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            JobTitle = request.JobTitle
        });

        return new CreateWorkHistoryResponse
        {
            WorkHistory = result,
            WorkHistoryId = result.Id
        };
    }
}