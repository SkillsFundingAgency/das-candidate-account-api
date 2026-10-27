using FluentAssertions;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.AdditionalQuestions;

public class WhenDeletingAllAdditionalQuestionsByApplicationId
{
    [Test, RecursiveMoqAutoData]
    public async Task TrainingCourses_Are_Deleted(
        Guid applicationId,
        List<AdditionalQuestionEntity> additionalQuestions,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] AdditionalQuestionRepository sut)
    {
        // arrange
        var expected = additionalQuestions.SkipLast(1).ToList();
        expected.ForEach(q => q.ApplicationId = applicationId);
        dataContext.Setup(x => x.AdditionalQuestionEntities).ReturnsDbSet(additionalQuestions);

        // act
        await sut.DeleteAllAsync(applicationId, CancellationToken.None);

        // assert
        dataContext.Object.AdditionalQuestionEntities.GetExecuteDeleteCalls().Should().ContainSingle().Which.Should().Equal(expected);
    }
}