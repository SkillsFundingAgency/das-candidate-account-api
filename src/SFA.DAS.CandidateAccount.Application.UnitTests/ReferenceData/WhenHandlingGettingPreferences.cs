using SFA.DAS.CandidateAccount.Application.ReferenceData.Queries.GetAvailablePreferences;
using SFA.DAS.CandidateAccount.Data.Preference;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.ReferenceData;

public class WhenHandlingGetAvailablePreferences
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Repository_Is_Called_And_Data_Returned(
        List<PreferenceEntity> data,
        [Frozen]Mock<IPreferencesRepository> repository,
        GetAvailablePreferencesQueryHandler handler)
    {
        repository.Setup(x => x.GetAll()).ReturnsAsync(data);
        
        var actual = await handler.Handle(new GetAvailablePreferencesQuery(), CancellationToken.None);

        actual.Preferences.Should().BeEquivalentTo(data.Select(c => (Preference)c).ToList());
    }
}