using FluentAssertions;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetAdditionalQuestion;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.ApiResponses;

[TestFixture]
public class WhenMappingGetAdditionalQuestionQueryResponseToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(GetAdditionalQuestionItemQueryResult source)
    {
        var actual = (GetAdditionalQuestionItemApiResponse)source;
        actual.Should().BeEquivalentTo(source);
    }
}