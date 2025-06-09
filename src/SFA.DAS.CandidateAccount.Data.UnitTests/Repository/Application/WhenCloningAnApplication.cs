using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application
{
    public class WhenCloningAnApplication
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Result_Has_Previous_Answers(
            ApplicationEntity originalApplication,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            ApplicationRepository repository)
        {
            var originalId = originalApplication.Id;

            context.Setup(x => x.ApplicationEntities)
                .ReturnsDbSet(new List<ApplicationEntity>{originalApplication});

            var actual = await repository.Clone(originalApplication.Id,
                originalApplication.VacancyReference,
                true,
                SectionStatus.NotRequired,
                SectionStatus.NotRequired,
                SectionStatus.NotStarted);

            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            actual.QualificationEntities.Should().BeEquivalentTo(originalApplication.QualificationEntities);
            actual.TrainingCourseEntities.Should().BeEquivalentTo(originalApplication.TrainingCourseEntities);
            actual.WorkHistoryEntities.Should().BeEquivalentTo(originalApplication.WorkHistoryEntities);

            actual.DisabilityConfidenceStatus.Should().Be((short)SectionStatus.PreviousAnswer);
            actual.JobsStatus.Should().Be((short)SectionStatus.PreviousAnswer);
            actual.AdditionalQuestion1Status.Should().Be((short)SectionStatus.NotRequired);
            actual.AdditionalQuestion2Status.Should().Be((short)SectionStatus.NotRequired);
            actual.InterestsStatus.Should().Be((short)SectionStatus.PreviousAnswer);
            actual.QualificationsStatus.Should().Be((short)SectionStatus.PreviousAnswer);
            actual.WorkExperienceStatus.Should().Be((short)SectionStatus.PreviousAnswer);
            actual.SkillsAndStrengthStatus.Should().Be((short)SectionStatus.PreviousAnswer);
            actual.TrainingCoursesStatus.Should().Be((short)SectionStatus.PreviousAnswer);
            actual.InterviewAdjustmentsStatus.Should().Be((short)SectionStatus.PreviousAnswer);
            actual.EmploymentLocationStatus.Should().Be((short)SectionStatus.NotStarted);

            actual.PreviousAnswersSourceId.Should().Be(originalId);
        }
    }
}
