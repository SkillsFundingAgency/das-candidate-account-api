using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate;

public class WhenGettingByEmail
{
    [Test, MoqAutoData]
    public async Task AndEmailExistsThenCandidateIsReturned(
        CandidateEntity candidate, 
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new[] { candidate });
            
        //Act
        var result = await repository.GetCandidateByEmail(candidate.Email);

        //Assert
        result.Should().BeEquivalentTo(candidate);
    }

    [Test, MoqAutoData]
    public async Task AndEmailDoesNotExistThenReturnsNull(
        CandidateEntity candidate, 
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new[] { candidate });

        //Act
        var result = await repository.GetCandidateByEmail("wrongEmail");

        //Assert
        result.Should().BeNull();
    }
}