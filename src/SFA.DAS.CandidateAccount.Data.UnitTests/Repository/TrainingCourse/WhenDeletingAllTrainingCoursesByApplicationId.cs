using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.TrainingCourse;

public class WhenDeletingAllTrainingCoursesByApplicationId
{
    [Test, RecursiveMoqAutoData]
    public async Task TrainingCourses_Are_Deleted(
        Guid applicationId,
        Guid candidateId,
        List<TrainingCourseEntity> trainingCourses,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] TrainingCourseRepository sut)
    {
        // arrange
        trainingCourses.ForEach(q =>
        {
            q.ApplicationId = applicationId;
            q.ApplicationEntity.CandidateId = candidateId;
        });
        dataContext.Setup(x => x.TrainingCourseEntities).ReturnsDbSet(trainingCourses);

        // act
        await sut.DeleteAllAsync(applicationId, candidateId, CancellationToken.None);

        // assert
        dataContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}