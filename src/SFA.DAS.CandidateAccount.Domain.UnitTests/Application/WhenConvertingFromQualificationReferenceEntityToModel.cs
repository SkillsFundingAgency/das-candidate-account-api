using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Application;

public class WhenConvertingFromQualificationReferenceEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(QualificationReferenceEntity source)
    {
        var actual = (QualificationReference)source;

        actual.Should().BeEquivalentTo(source, options => options.ExcludingMissingMembers());
    }
}