using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Qualification;

public class WhenDeletingAllQualificationsByApplicationId
{
    [Test, RecursiveMoqAutoData]
    public async Task Qualifications_Are_Deleted(
        Guid applicationId,
        Guid candidateId,
        List<QualificationEntity> qualifications,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] QualificationRepository sut)
    {
        // arrange
        qualifications.ForEach(q =>
        {
            q.ApplicationId = applicationId;
            q.ApplicationEntity.CandidateId = candidateId;
        });
        dataContext.Setup(x => x.QualificationEntities).ReturnsDbSet(qualifications);

        // act
        await sut.DeleteAllAsync(applicationId, candidateId, CancellationToken.None);

        // assert
        dataContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}