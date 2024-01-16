using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Candidate;

public class WhenConvertingFromCandidateEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(CandidateEntity source)
    {
        var actual = (Domain.Candidate.Candidate)source;

        actual.Should().BeEquivalentTo(source, options=>options.Excluding(c=>c.Applications));
        actual.Applications.Should().BeEquivalentTo(source.Applications.Select(c => (Domain.Application.Application)c));
    }
}