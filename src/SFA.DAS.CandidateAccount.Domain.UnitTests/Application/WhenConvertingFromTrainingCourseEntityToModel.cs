using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Application;
public class WhenConvertingFromTrainingCourseEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(TrainingCourseEntity source)
    {
        var actual = (TrainingCourse)source;

        actual.Id.Should().Be(source.Id);
        actual.ApplicationId.Should().Be(source.ApplicationId);
        actual.Title.Should().Be(source.Title);
        actual.ToYear.Should().Be(source.ToYear);
    }
}
