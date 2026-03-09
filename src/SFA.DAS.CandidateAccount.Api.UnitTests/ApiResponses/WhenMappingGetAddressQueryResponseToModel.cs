using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetAddress;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.ApiResponses;

public class WhenMappingGetAddressQueryResponseToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(GetAddressQueryResult source)
    {
        var actual = (GetAddressApiResponse)source;
        actual.Should().BeEquivalentTo(source.Address);
    }
}