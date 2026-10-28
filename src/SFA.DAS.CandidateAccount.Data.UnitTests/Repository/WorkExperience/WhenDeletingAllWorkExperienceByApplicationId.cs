using FluentAssertions;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.WorkExperience;

public class WhenDeletingAllWorkExperienceByApplicationId
{
    [Test, RecursiveMoqAutoData]
    public async Task TrainingCourses_Are_Deleted(
        Guid applicationId,
        List<WorkHistoryEntity> workHistories,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] WorkHistoryRepository sut)
    {
        // arrange
        var expected = workHistories.SkipLast(1).ToList();
        expected.ForEach(q => q.ApplicationId = applicationId);
        dataContext.Setup(x => x.WorkExperienceEntities).ReturnsDbSet(workHistories);

        // act
        await sut.DeleteAllAsync(applicationId, CancellationToken.None);

        // assert
        dataContext.Object.WorkExperienceEntities.GetExecuteDeleteCalls().Should().ContainSingle().Which.Should().Equal(expected);
    }
}