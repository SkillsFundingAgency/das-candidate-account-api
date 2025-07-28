using SFA.DAS.CandidateAccount.Data.EmploymentLocation;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.EmploymentLocationRepositoryTests;

public class WhenDeletingAllEmploymentLocationsByApplicationId
{
    [Test, RecursiveMoqAutoData]
    public async Task Qualifications_Are_Deleted(
        Guid applicationId,
        Guid candidateId,
        List<EmploymentLocationEntity> locations,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] EmploymentLocationRepository sut)
    {
        // arrange
        locations.ForEach(q =>
        {
            q.ApplicationId = applicationId;
            q.ApplicationEntity.CandidateId = candidateId;
        });
        dataContext.Setup(x => x.EmploymentLocationEntities).ReturnsDbSet(locations);

        // act
        await sut.DeleteAllAsync(applicationId, candidateId, CancellationToken.None);

        // assert
        dataContext.Verify(x => x.EmploymentLocationEntities.RemoveRange(locations), Times.Once);
        dataContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}