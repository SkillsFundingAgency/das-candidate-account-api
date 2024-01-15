namespace SFA.DAS.CandidateAccount.Application.ApplicationTemplate.Commands.CreateApplication;

public class CreateApplicationResponse
{
    public Domain.Application.ApplicationTemplate ApplicationTemplate { get; set; }
    public bool IsCreated { get; set; }
}