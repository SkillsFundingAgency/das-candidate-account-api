using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application;

public class WhenUpdatingAnApplication
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Updated(
        ApplicationEntity entity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        context.Setup(x => x.ApplicationEntities)
            .ReturnsDbSet(new List<ApplicationEntity> { entity });
        var actual = await repository.Update(entity);

        actual.Should().BeEquivalentTo(entity);
        context.Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}