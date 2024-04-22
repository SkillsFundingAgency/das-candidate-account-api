using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.AdditionalQuestion;

[TestFixture]
public class WhenConvertingFromAdditionalQuestionEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(AdditionalQuestionEntity source)
    {
        var actual = (Domain.Application.AdditionalQuestion)source;
        actual.Should().BeEquivalentTo(source, options => options.Excluding(c=>c.ApplicationEntity));
    }
}