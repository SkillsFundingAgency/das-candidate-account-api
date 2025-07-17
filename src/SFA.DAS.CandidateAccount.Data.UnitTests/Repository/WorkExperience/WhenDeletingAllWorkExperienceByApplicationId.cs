using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.WorkExperience;

public class WhenDeletingAllWorkExperienceByApplicationId
{
    [Test, RecursiveMoqAutoData]
    public async Task TrainingCourses_Are_Deleted(
        Guid applicationId,
        Guid candidateId,
        List<WorkHistoryEntity> workHistories,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] WorkHistoryRepository sut)
    {
        // arrange
        workHistories.ForEach(q =>
        {
            q.ApplicationId = applicationId;
            q.ApplicationEntity.CandidateId = candidateId;
        });
        dataContext.Setup(x => x.WorkExperienceEntities).ReturnsDbSet(workHistories);

        // act
        await sut.DeleteAllAsync(applicationId, candidateId, CancellationToken.None);

        // assert
        dataContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}