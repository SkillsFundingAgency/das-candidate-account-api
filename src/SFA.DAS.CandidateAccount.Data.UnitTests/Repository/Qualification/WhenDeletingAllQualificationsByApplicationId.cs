using FluentAssertions;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Qualification;

public class WhenDeletingAllQualificationsByApplicationId
{
    [Test, RecursiveMoqAutoData]
    public async Task Qualifications_Are_Deleted(
        Guid applicationId,
        List<QualificationEntity> qualifications,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] QualificationRepository sut)
    {
        // arrange
        var expected = qualifications.SkipLast(1).ToList();
        expected.ForEach(q => q.ApplicationId = applicationId);
        dataContext.Setup(x => x.QualificationEntities).ReturnsDbSet(qualifications);

        // act
        await sut.DeleteAllAsync(applicationId, CancellationToken.None);

        // assert
        dataContext.Object.QualificationEntities.GetExecuteDeleteCalls().Should().ContainSingle().Which.Should().Equal(expected);
    }
}