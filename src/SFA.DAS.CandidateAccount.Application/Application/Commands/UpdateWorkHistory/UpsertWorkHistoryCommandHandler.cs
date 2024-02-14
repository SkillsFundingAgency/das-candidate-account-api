using MediatR;
using SFA.DAS.CandidateAccount.Data.WorkExperience;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateWorkHistory;

public class UpsertWorkHistoryCommandHandler(IWorkHistoryRepository repository) : IRequestHandler<UpsertWorkHistoryCommand, UpsertWorkHistoryCommandResponse>
{
    public async Task<UpsertWorkHistoryCommandResponse> Handle(UpsertWorkHistoryCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.UpsertWorkHistory(request.WorkHistory, request.CandidateId);

        return new UpsertWorkHistoryCommandResponse
        {
            WorkHistory = result.Item1,
            IsCreated = result.Item2
        };
    }
}