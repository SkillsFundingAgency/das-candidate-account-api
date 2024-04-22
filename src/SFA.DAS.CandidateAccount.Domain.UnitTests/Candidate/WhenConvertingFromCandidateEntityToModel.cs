using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Candidate;

public class WhenConvertingFromCandidateEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(CandidateEntity source, CandidateStatus status)
    {
        source.Status = (short)status;
        var actual = (Domain.Candidate.Candidate)source;

        actual.Should().BeEquivalentTo(source, options=>options.Excluding(c=>c.Applications).Excluding(c => c.Status));
        actual.Status.Should().Be(status);
    }
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped_With_No_Applications(CandidateEntity source, CandidateStatus status)
    {
        source.Status = (short)status;
        source.Applications = null;
        
        var actual = (Domain.Candidate.Candidate)source;

        actual.Should().BeEquivalentTo(source, options=>options.Excluding(c=>c.Applications).Excluding(c => c.Status));
        actual.Status.Should().Be(status);
    }
}