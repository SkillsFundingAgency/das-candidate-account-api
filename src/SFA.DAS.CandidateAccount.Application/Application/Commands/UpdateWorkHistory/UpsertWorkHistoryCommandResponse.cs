namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateWorkHistory;
public class UpsertWorkHistoryCommandResponse
{
    public required Domain.Application.WorkHistory WorkHistory { get; set; }
    public bool IsCreated { get; set; }
}
