using AutoFixture.NUnit3;
using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Candidate;

public class WhenConvertingFromCandidateToCandidateEntity
{
    [Test, AutoData]
    public void Then_The_Fields_Are_Mapped(Domain.Candidate.Candidate source)
    {
        var actual = (CandidateEntity)source;

        actual.Should().BeEquivalentTo(source, options=>options.Excluding(c=>c.Applications));
    }
}