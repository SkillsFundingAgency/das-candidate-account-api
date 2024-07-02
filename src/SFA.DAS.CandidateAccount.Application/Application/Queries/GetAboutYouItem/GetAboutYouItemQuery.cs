using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetAboutYouItem;
public class GetAboutYouItemQuery : IRequest<GetAboutYouItemQueryResult>
{
    public Guid CandidateId { get; set; }
}
