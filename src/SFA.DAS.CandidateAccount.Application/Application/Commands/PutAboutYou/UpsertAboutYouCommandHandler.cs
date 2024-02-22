using MediatR;
using SFA.DAS.CandidateAccount.Data.AboutYou;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateSkillsAndStrengths;
public class UpsertAboutYouCommandHandler(IAboutYouRespository aboutYouRespository)
    : IRequestHandler<UpsertAboutYouCommand, UpsertAboutYouCommandResult>
{
    public async Task<UpsertAboutYouCommandResult> Handle(UpsertAboutYouCommand request, CancellationToken cancellationToken)
    {
        var result = await aboutYouRespository.Upsert(request.AboutYou, request.CandidateId);
        return new UpsertAboutYouCommandResult
        {
            AboutYou = result.Item1,
            IsCreated = result.Item2
        };
    }
}
