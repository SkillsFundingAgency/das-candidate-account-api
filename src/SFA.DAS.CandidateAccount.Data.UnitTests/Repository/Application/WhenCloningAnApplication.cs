﻿using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain;
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
                SectionStatus.NotStarted,
                ApprenticeshipTypes.Standard);

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
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_If_Applying_For_A_Foundation_Apprenticeship_Then_The_Skills_Are_Not_Cloned(
            ApplicationEntity originalApplication,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            ApplicationRepository repository)
        {
            // arrange
            context.Setup(x => x.ApplicationEntities).ReturnsDbSet(new List<ApplicationEntity>{originalApplication});

            // act
            var actual = await repository.Clone(originalApplication.Id,
                originalApplication.VacancyReference,
                true,
                SectionStatus.NotRequired,
                SectionStatus.NotRequired,
                SectionStatus.NotRequired,
                ApprenticeshipTypes.Foundation);

            // assert
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            actual.SkillsAndStrengthStatus.Should().Be((short)SectionStatus.NotRequired);
            actual.Strengths.Should().BeNull();
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_If_Applying_For_An_Apprenticeship_Standard_And_Copying_Answers_From_A_Foundation_Apprenticeship_Application_Then_The_Skills_Status_Is_NotStarted(
            ApplicationEntity originalApplication,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            ApplicationRepository repository)
        {
            // arrange
            originalApplication.Strengths = null;
            context.Setup(x => x.ApplicationEntities).ReturnsDbSet(new List<ApplicationEntity>{originalApplication});

            // act
            var actual = await repository.Clone(originalApplication.Id,
                originalApplication.VacancyReference,
                true,
                SectionStatus.NotRequired,
                SectionStatus.NotRequired,
                SectionStatus.NotStarted,
                ApprenticeshipTypes.Standard);

            // assert
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            actual.SkillsAndStrengthStatus.Should().Be((short)SectionStatus.NotStarted);
            actual.Strengths.Should().BeNull();
        }
    }
}
