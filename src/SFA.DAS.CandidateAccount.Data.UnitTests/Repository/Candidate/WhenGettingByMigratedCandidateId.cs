using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate;

public class WhenGettingByMigratedCandidateId
{
    [Test, RecursiveMoqAutoData]
    public async Task AndExistsThenCandidateIsReturned(
        CandidateEntity candidate, 
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new[] { candidate });
            
        //Act
        var result = await repository.GetByMigratedCandidateId(candidate.MigratedCandidateId);

        //Assert
        result.Should().BeEquivalentTo(candidate);
    }

    [Test, RecursiveMoqAutoData]
    public async Task AndIdDoesNotExistThenReturnsNull(
        CandidateEntity candidate, 
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new[] { candidate });

        //Act
        var result = await repository.GetByMigratedCandidateId(Guid.NewGuid());

        //Assert
        result.Should().BeNull();
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task AndIdIsNullThenReturnsNull(
        CandidateEntity candidate, 
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new[] { candidate });

        //Act
        var result = await repository.GetByMigratedCandidateId(null);

        //Assert
        result.Should().BeNull();
    }
}