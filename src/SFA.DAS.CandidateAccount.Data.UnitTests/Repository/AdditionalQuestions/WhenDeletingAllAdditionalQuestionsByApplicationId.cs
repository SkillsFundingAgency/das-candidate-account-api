using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.AdditionalQuestions;

public class WhenDeletingAllAdditionalQuestionsByApplicationId
{
    [Test, RecursiveMoqAutoData]
    public async Task TrainingCourses_Are_Deleted(
        Guid applicationId,
        Guid candidateId,
        List<AdditionalQuestionEntity> additionalQuestions,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] AdditionalQuestionRepository sut)
    {
        // arrange
        additionalQuestions.ForEach(q =>
        {
            q.ApplicationId = applicationId;
            q.ApplicationEntity.CandidateId = candidateId;
        });
        dataContext.Setup(x => x.AdditionalQuestionEntities).ReturnsDbSet(additionalQuestions);

        // act
        await sut.DeleteAllAsync(applicationId, candidateId, CancellationToken.None);

        // assert
        dataContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}