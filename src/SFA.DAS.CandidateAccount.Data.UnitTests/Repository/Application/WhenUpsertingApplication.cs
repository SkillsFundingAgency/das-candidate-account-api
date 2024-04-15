using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application;

public class WhenUpsertingApplication
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Inserted_If_Not_Exists(
        ApplicationEntity applicationEntity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        //Arrange
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(new List<ApplicationEntity>());
            
        //Act
        var actual = await repository.Upsert(applicationEntity);

        //Assert
        context.Verify(x => x.ApplicationEntities.AddAsync(applicationEntity, CancellationToken.None), Times.Once);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        actual.Item2.Should().BeTrue();
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Updated_If_Exists(
        ApplicationEntity applicationEntity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        //Arrange
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(new List<ApplicationEntity>{applicationEntity});
            
        //Act
        var actual = await repository.Upsert(applicationEntity);

        //Assert
        context.Verify(x => x.ApplicationEntities.AddAsync(It.IsAny<ApplicationEntity>(), CancellationToken.None), Times.Never);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        actual.Item2.Should().BeFalse();
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Statuses_Are_Updated_If_They_Are_Not_NotStarted(
        ApplicationEntity updateEntity,
        ApplicationEntity applicationEntity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        //Arrange
        updateEntity.QualificationsStatus = 2;
        updateEntity.TrainingCoursesStatus = 2;
        updateEntity.JobsStatus = 2;
        updateEntity.WorkExperienceStatus = 2;
        updateEntity.SkillsAndStrengthStatus = 2;
        updateEntity.InterestsStatus = 2;
        updateEntity.AdditionalQuestion1Status = 2;
        updateEntity.AdditionalQuestion2Status = 2;
        updateEntity.InterviewAdjustmentsStatus = 2;
        updateEntity.DisabilityConfidenceStatus = 2;

        applicationEntity.VacancyReference = updateEntity.VacancyReference;
        applicationEntity.CandidateId = updateEntity.CandidateId;
        applicationEntity.QualificationsStatus = 0;
        applicationEntity.TrainingCoursesStatus = 0;
        applicationEntity.JobsStatus = 0;
        applicationEntity.WorkExperienceStatus = 0;
        applicationEntity.SkillsAndStrengthStatus = 0;
        applicationEntity.InterestsStatus = 0;
        applicationEntity.AdditionalQuestion1Status = 0;
        applicationEntity.AdditionalQuestion2Status = 0;
        applicationEntity.InterviewAdjustmentsStatus = 0;
        applicationEntity.DisabilityConfidenceStatus = 0;
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(new List<ApplicationEntity>{applicationEntity});
        
        //Act
        var actual = await repository.Upsert(updateEntity);
        
        //Assert
        actual.Item1.QualificationsStatus.Should().Be(2);
        actual.Item1.TrainingCoursesStatus.Should().Be(2);
        actual.Item1.JobsStatus.Should().Be(2);
        actual.Item1.WorkExperienceStatus.Should().Be(2);
        actual.Item1.SkillsAndStrengthStatus.Should().Be(2);
        actual.Item1.InterestsStatus.Should().Be(2);
        actual.Item1.AdditionalQuestion1Status.Should().Be(2);
        actual.Item1.AdditionalQuestion2Status.Should().Be(2);
        actual.Item1.InterviewAdjustmentsStatus.Should().Be(2);
        actual.Item1.DisabilityConfidenceStatus.Should().Be(2);
        actual.Item2.Should().BeFalse();
    }
}