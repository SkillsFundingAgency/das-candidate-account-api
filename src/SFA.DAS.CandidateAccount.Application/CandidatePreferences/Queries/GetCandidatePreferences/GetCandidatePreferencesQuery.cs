using MediatR;

namespace SFA.DAS.CandidateAccount.Application.CandidatePreferences.Queries.GetCandidatePreferences;
public class GetCandidatePreferencesQuery : IRequest<GetCandidatePreferencesQueryResult>
{
    public Guid CandidateId { get; set; }
}
