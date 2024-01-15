using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.ApplicationTemplate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.ApplicationTemplate;

public class WhenUpsertingApplicationTemplate
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Inserted_If_Not_Exists(
        ApplicationTemplateEntity applicationTemplateEntity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationTemplateRepository repository)
    {
        //Arrange
        context.Setup(x => x.ApplicationTemplateEntities).ReturnsDbSet(new List<ApplicationTemplateEntity>());
            
        //Act
        var actual = await repository.Upsert(applicationTemplateEntity);

        //Assert
        context.Verify(x => x.ApplicationTemplateEntities.AddAsync(applicationTemplateEntity, CancellationToken.None), Times.Once);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        actual.Item2.Should().BeTrue();
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Updated_If_Not_Exists(
        ApplicationTemplateEntity applicationTemplateEntity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationTemplateRepository repository)
    {
        //Arrange
        context.Setup(x => x.ApplicationTemplateEntities).ReturnsDbSet(new List<ApplicationTemplateEntity>{applicationTemplateEntity});
            
        //Act
        var actual = await repository.Upsert(applicationTemplateEntity);

        //Assert
        context.Verify(x => x.ApplicationTemplateEntities.AddAsync(It.IsAny<ApplicationTemplateEntity>(), CancellationToken.None), Times.Never);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        actual.Item2.Should().BeFalse();
    }
}