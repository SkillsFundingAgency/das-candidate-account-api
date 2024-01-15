namespace SFA.DAS.CandidateAccount.Application.Application.Commands.CreateApplication;

public class CreateApplicationResponse
{
    public Domain.Application.Application Application { get; set; }
    public bool IsCreated { get; set; }
}