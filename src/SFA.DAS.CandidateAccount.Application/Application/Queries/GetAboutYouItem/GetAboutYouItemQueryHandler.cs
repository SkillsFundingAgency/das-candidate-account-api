using MediatR;
using SFA.DAS.CandidateAccount.Data.AboutYou;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetAboutYouItem;
public class GetAboutYouItemQueryHandler(IAboutYouRespository aboutYouRespository) : IRequestHandler<GetAboutYouItemQuery, GetAboutYouItemQueryResult>
{
    public async Task<GetAboutYouItemQueryResult> Handle(GetAboutYouItemQuery request, CancellationToken cancellationToken)
    {
        var aboutYou = await aboutYouRespository.Get(request.ApplicationId, request.CandidateId);

        if (aboutYou is null) return new GetAboutYouItemQueryResult();

        return aboutYou;
    }
}
