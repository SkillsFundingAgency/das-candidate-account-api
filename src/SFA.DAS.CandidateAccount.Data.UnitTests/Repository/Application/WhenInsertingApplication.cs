using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application;

public class WhenInsertingApplication
{
    [Test, RecursiveMoqAutoData]
    public async Task ThenTheApplicationIsInserted(
        ApplicationEntity applicationEntity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        //Arrange
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(new List<ApplicationEntity>());
            
        //Act
        await repository.Insert(applicationEntity);

        //Assert
        context.Verify(x => x.ApplicationEntities.AddAsync(applicationEntity, CancellationToken.None), Times.Once);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}