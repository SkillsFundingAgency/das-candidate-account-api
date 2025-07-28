namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteApplication;

public record DeleteApplicationCommandResult(Guid ApplicationId)
{
    public static readonly DeleteApplicationCommandResult None = new(Guid.Empty);
}