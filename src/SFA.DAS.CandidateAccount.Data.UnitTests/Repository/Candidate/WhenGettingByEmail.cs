using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate;

public class WhenGettingByEmail
{
    [Test]
    [RecursiveMoqInlineAutoData(CandidateStatus.Completed)]
    [RecursiveMoqInlineAutoData(CandidateStatus.InProgress)]
    [RecursiveMoqInlineAutoData(CandidateStatus.Incomplete)]
    public async Task And_Email_Exists_Then_Candidate_Is_Returned(
        CandidateStatus status,
        CandidateEntity candidate, 
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        candidate.Status = (short)status;
        context.Setup(x => x.CandidateEntities).ReturnsDbSet([candidate]);
            
        //Act
        var result = await repository.GetCandidateByEmail(candidate.Email);

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
        var result = await repository.GetCandidateByEmail("wrongEmail");

        //Assert
        result.Should().BeNull();
    }

    [Test, RecursiveMoqAutoData]
    public async Task And_Status_Deleted_Email_Exists_Then_Null_Is_Returned(
        CandidateEntity candidate,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        candidate.Status = (short)CandidateStatus.Deleted;
        context.Setup(x => x.CandidateEntities).ReturnsDbSet([candidate]);

        //Act
        var result = await repository.GetCandidateByEmail(candidate.Email);

        //Assert
        result.Should().BeNull();
    }
}