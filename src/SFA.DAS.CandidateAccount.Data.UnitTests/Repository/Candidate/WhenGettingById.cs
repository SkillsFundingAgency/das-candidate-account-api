using FluentAssertions;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate;

public class WhenGettingById
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
        var result = await repository.GetById(candidate.Id);

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
        var result = await repository.GetById(Guid.NewGuid());

        //Assert
        result.Should().BeNull();
    }
}