
using FluentAssertions;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application;

public class WhenDeletingAnApplication
{
    [Test, RecursiveMoqAutoData]
    public async Task The_Application_Is_Deleted(
        Guid candidateId,
        List<ApplicationEntity> applications,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        [Greedy] ApplicationRepository sut)
    {
        // arrange
        dataContext.Setup(x => x.ApplicationEntities).ReturnsDbSet(applications);
        applications.ForEach(x => x.CandidateId = candidateId);

        // act
        var result = await sut.DeleteAsync(applications.First().Id, candidateId, CancellationToken.None);

        // assert
        dataContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        result.Should().BeTrue();
    }
}