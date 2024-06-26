using MediatR;
using SFA.DAS.CandidateAccount.Data.Preference;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Application.ReferenceData.Queries.GetAvailablePreferences;

public class GetAvailablePreferencesQueryHandler(IPreferencesRepository preferencesRepository) : IRequestHandler<GetAvailablePreferencesQuery, GetAvailablePreferencesQueryResult>
{
    public async Task<GetAvailablePreferencesQueryResult> Handle(GetAvailablePreferencesQuery request, CancellationToken cancellationToken)
    {
        var result = await preferencesRepository.GetAll();

        return new GetAvailablePreferencesQueryResult
        {
            Preferences = result.Select(c=>(Preference)c).ToList()
        };
    }
}