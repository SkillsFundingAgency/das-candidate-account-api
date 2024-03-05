namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateSkillsAndStrengths;
public class UpsertAboutYouCommandResult
{
    public required Domain.Candidate.AboutYou AboutYou { get; set; }
    public bool IsCreated { get; set; }
}
