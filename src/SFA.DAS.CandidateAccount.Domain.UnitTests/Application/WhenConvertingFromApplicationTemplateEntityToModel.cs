using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Application;

public class WhenConvertingFromApplicationTemplateEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(ApplicationTemplateEntity source)
    {
        var actual = (ApplicationTemplate)source;

        actual.Should().BeEquivalentTo(source, options => options
            .Excluding(c=>c.CreatedDate)
            .Excluding(c=>c.UpdatedDate)
            .Excluding(c=>c.CandidateEntity)
        );
    }
}