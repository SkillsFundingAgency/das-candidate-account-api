using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateSkillsAndStrengths;
public class UpsertAboutYouCommand : IRequest<UpsertAboutYouCommandResult>
{
    public Guid CandidateId { get; set; }
    public Domain.Candidate.AboutYou AboutYou { get; set; }
}
