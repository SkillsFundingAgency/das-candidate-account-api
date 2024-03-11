using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Application;

public class WhenConvertingFromQualificationEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(QualificationEntity source)
    {
        var actual = (Qualification)source;

        actual.Should().BeEquivalentTo(source, options => options.ExcludingMissingMembers());
    }
}