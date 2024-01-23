using MediatR;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.CreateJob;

public class CreateWorkHistoryCommandHandler(ICandidateRepository candidateRepository) :
    IRequestHandler<CreateWorkHistoryCommand, CreateWorkHistoryResponse>
{
    public async Task<CreateWorkHistoryResponse> Handle(CreateWorkHistoryCommand request, CancellationToken cancellationToken)
    {
        return new CreateWorkHistoryResponse
        {
            WorkHistoryId = Guid.NewGuid(),
            WorkHistory = new WorkExperienceEntity
            {
                ApplicationId = request.ApplicationId
            }
        };
    }
}