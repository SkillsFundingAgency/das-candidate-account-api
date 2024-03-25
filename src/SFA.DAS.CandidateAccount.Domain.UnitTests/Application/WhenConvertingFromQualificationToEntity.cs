using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Application;

public class WhenConvertingFromQualificationToEntity
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(Qualification source)
    {
        var actual = (QualificationEntity)source;

        actual.Should().BeEquivalentTo(source, options => options.ExcludingMissingMembers());
    }
}