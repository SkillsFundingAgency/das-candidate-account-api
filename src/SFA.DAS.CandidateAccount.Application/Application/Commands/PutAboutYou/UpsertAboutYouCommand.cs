using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.PutAboutYou;
public class UpsertAboutYouCommand : IRequest<UpsertAboutYouCommandResult>
{
    public Guid CandidateId { get; set; }
    public Domain.Candidate.AboutYou AboutYou { get; set; }
}
