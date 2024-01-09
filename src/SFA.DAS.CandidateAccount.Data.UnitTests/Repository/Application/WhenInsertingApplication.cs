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
        ApplicationTemplateEntity applicationTemplateEntity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationTemplateRepository repository)
    {
        //Arrange
        context.Setup(x => x.ApplicationTemplateEntities).ReturnsDbSet(new List<ApplicationTemplateEntity>());
            
        //Act
        await repository.Insert(applicationTemplateEntity);

        //Assert
        context.Verify(x => x.ApplicationTemplateEntities.AddAsync(applicationTemplateEntity, CancellationToken.None), Times.Once);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}