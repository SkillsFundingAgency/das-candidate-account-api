using FluentAssertions;
using SFA.DAS.CandidateAccount.Data.EmploymentLocation;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.EmploymentLocationRepositoryTests;

public class WhenDeletingAllEmploymentLocationsByApplicationId
{
    [Test, RecursiveMoqAutoData]
    public async Task Qualifications_Are_Deleted(
        Guid applicationId,
        List<EmploymentLocationEntity> locations,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] EmploymentLocationRepository sut)
    {
        // arrange
        var expected = locations.SkipLast(1).ToList();
        expected.ForEach(q => q.ApplicationId = applicationId);
        dataContext.Setup(x => x.EmploymentLocationEntities).ReturnsDbSet(locations);

        // act
        await sut.DeleteAllAsync(applicationId, CancellationToken.None);

        // assert
        dataContext.Object.EmploymentLocationEntities.GetExecuteDeleteCalls().Should().ContainSingle().Which.Should().Equal(expected);
    }
}