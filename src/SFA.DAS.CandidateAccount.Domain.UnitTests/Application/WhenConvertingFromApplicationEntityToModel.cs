using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Application;

public class WhenConvertingFromApplicationEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(ApplicationEntity source)
    {
        var actual = (Domain.Application.Application)source;

        actual.Should().BeEquivalentTo(source, options => options
            .Excluding(c=>c.CreatedDate)
            .Excluding(c=>c.UpdatedDate)
            .Excluding(c=>c.CandidateEntity)
        );
    }
}