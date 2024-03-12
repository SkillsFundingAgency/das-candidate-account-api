using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Qualification;

public class WhenUpsertingQualification
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Qualification_Is_Inserted_If_Not_Exists(
        Domain.Application.Qualification qualification,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        QualificationRepository repository)
    {
        //Arrange
        context.Setup(x => x.QualificationEntities).ReturnsDbSet(new List<QualificationEntity>());
            
        //Act
        var actual = await repository.Upsert(qualification);

        //Assert
        context.Verify(x => x.QualificationEntities.AddAsync(It.Is<QualificationEntity>(c=>c.Id == qualification.Id), CancellationToken.None), Times.Once);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        actual.Item2.Should().BeTrue();
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Qualification_Is_Updated_If_Exists(
        QualificationEntity qualificationEntity,
        Domain.Application.Qualification qualification,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        QualificationRepository repository)
    {
        //Arrange
        qualification.Id = qualificationEntity.Id;
        qualification.Application.Id = qualificationEntity.ApplicationId;
        qualification.Application.CandidateId = qualificationEntity.ApplicationEntity.CandidateId; 
        context.Setup(x => x.QualificationEntities).ReturnsDbSet(new List<QualificationEntity>{qualificationEntity});
            
        //Act
        var actual = await repository.Upsert(qualification);

        //Assert
        context.Verify(x => x.QualificationEntities.AddAsync(It.IsAny<QualificationEntity>(), CancellationToken.None), Times.Never);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        actual.Item2.Should().BeFalse();
    }
}