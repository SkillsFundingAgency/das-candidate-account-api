using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Application;

public class WhenConvertingFromApplicationEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(ApplicationEntity source)
    {
        source.IsApplicationQuestionsComplete = 3;
        source.IsEducationHistoryComplete = 2;
        source.IsInterviewAdjustmentsComplete = 1;
        source.IsWorkHistoryComplete = 0;
        source.IsDisabilityConfidenceComplete = 1;
        source.Status = 2;
        
        var actual = (Domain.Application.Application)source;

        actual.Should().BeEquivalentTo(source, options => options
            .Excluding(c=>c.CreatedDate)
            .Excluding(c=>c.UpdatedDate)
            .Excluding(c=>c.CandidateEntity)
            .Excluding(c=>c.IsApplicationQuestionsComplete)
            .Excluding(c=>c.IsEducationHistoryComplete)
            .Excluding(c=>c.IsInterviewAdjustmentsComplete)
            .Excluding(c=>c.IsWorkHistoryComplete)
            .Excluding(c=>c.IsDisabilityConfidenceComplete)
            .Excluding(c=>c.Status)
        );
        actual.IsApplicationQuestionsComplete.Should().Be(SectionStatus.NotRequired);
        actual.IsEducationHistoryComplete.Should().Be(SectionStatus.Completed);
        actual.IsInterviewAdjustmentsComplete.Should().Be(SectionStatus.InProgress);
        actual.IsWorkHistoryComplete.Should().Be(SectionStatus.NotStarted);
        actual.IsDisabilityConfidenceComplete.Should().Be(SectionStatus.InProgress);
        actual.Status.Should().Be(ApplicationStatus.Withdrawn);
    }
}