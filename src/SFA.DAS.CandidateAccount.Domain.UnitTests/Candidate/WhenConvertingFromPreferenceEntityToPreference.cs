using AutoFixture.NUnit3;
using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Candidate;

public class WhenConvertingFromPreferenceEntityToPreference
{
    [Test, AutoData]
    public void Then_The_Fields_Are_Mapped(PreferenceEntity source)
    {
        var actual = (Preference)source;

        actual.Should().BeEquivalentTo(source);
    }
}