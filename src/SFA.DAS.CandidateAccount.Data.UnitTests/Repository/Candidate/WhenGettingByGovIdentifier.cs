using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate;

public class WhenGettingByGovIdentifier
{
    [Test, RecursiveMoqAutoData]
    public async Task AndGovIdentifierExistsThenCandidateIsReturned(
        CandidateEntity candidate, 
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new[] { candidate });
            
        //Act
        var result = await repository.GetByGovIdentifier(candidate.GovUkIdentifier);

        //Assert
        result.Should().BeEquivalentTo(candidate);
    }

    [Test, RecursiveMoqAutoData]
    public async Task AndEmailDoesNotExistThenReturnsNull(
        CandidateEntity candidate, 
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new[] { candidate });

        //Act
        var result = await repository.GetByGovIdentifier("wrongGovIdentifier");

        //Assert
        result.Should().BeNull();
    }
}