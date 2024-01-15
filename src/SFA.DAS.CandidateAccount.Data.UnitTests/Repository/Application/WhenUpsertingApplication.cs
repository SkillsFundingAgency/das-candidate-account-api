using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application;

public class WhenUpsertingApplication
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Inserted_If_Not_Exists(
        ApplicationEntity applicationEntity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        //Arrange
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(new List<ApplicationEntity>());
            
        //Act
        var actual = await repository.Upsert(applicationEntity);

        //Assert
        context.Verify(x => x.ApplicationEntities.AddAsync(applicationEntity, CancellationToken.None), Times.Once);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        actual.Item2.Should().BeTrue();
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Updated_If_Not_Exists(
        ApplicationEntity applicationEntity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        //Arrange
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(new List<ApplicationEntity>{applicationEntity});
            
        //Act
        var actual = await repository.Upsert(applicationEntity);

        //Assert
        context.Verify(x => x.ApplicationEntities.AddAsync(It.IsAny<ApplicationEntity>(), CancellationToken.None), Times.Never);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        actual.Item2.Should().BeFalse();
    }
}