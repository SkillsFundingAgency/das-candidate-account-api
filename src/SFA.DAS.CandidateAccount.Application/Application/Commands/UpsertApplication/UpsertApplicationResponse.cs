namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;

public class UpsertApplicationResponse
{
    public Domain.Application.Application Application { get; set; }
    public bool IsCreated { get; set; }
}