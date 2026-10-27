using FluentAssertions;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.TrainingCourse;

public class WhenDeletingAllTrainingCoursesByApplicationId
{
    [Test, RecursiveMoqAutoData]
    public async Task TrainingCourses_Are_Deleted(
        Guid applicationId,
        List<TrainingCourseEntity> trainingCourses,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] TrainingCourseRepository sut)
    {
        // arrange
        var expected = trainingCourses.SkipLast(1).ToList();
        expected.ForEach(q => q.ApplicationId = applicationId);
        dataContext.Setup(x => x.TrainingCourseEntities).ReturnsDbSet(trainingCourses);

        // act
        await sut.DeleteAllAsync(applicationId, CancellationToken.None);

        // assert
        dataContext.Object.TrainingCourseEntities.GetExecuteDeleteCalls().Should().ContainSingle().Which.Should().Equal(expected);
    }
}