using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate;

public class WhenUpdatingById
{
    [Test, RecursiveMoqAutoData]
    public async Task AndIdExistsThenCandidateIsUpdated(
        Domain.Candidate.Candidate existingCandidate,
        CandidateEntity candidate,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities)
            .ReturnsDbSet(new List<CandidateEntity> { candidate });
        existingCandidate.Id = candidate.Id;

        //Act
        var actual = await repository.UpdateCandidateById(existingCandidate);

        //Assert
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        actual.FirstName.Should().Be(existingCandidate.FirstName);
        actual.LastName.Should().Be(existingCandidate.LastName);
        actual.GovUkIdentifier.Should().Be(candidate.GovUkIdentifier);
        actual.UpdatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }
    [Test, RecursiveMoqAutoData]
    public async Task AndIdExistsThenCandidateIsUpdatedIfNotNull(
        Domain.Candidate.Candidate existingCandidate,
        CandidateEntity candidate,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities)
            .ReturnsDbSet(new List<CandidateEntity> { candidate });
        existingCandidate.Id = candidate.Id;
        existingCandidate.FirstName = null;
        existingCandidate.LastName = null;

        //Act
        var actual = await repository.UpdateCandidateById(existingCandidate);

        //Assert
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        actual.FirstName.Should().Be(candidate.FirstName);
        actual.LastName.Should().Be(candidate.LastName);
        actual.Email.Should().Be(existingCandidate.Email);
        actual.GovUkIdentifier.Should().Be(candidate.GovUkIdentifier);
        actual.UpdatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Test, RecursiveMoqAutoData]
    public async Task AndIdDoesNotExistThenNoUpdateIsMade(
        CandidateEntity candidate,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities)
            .ReturnsDbSet(new List<CandidateEntity> { candidate });
        var noCandidateExists = new Domain.Candidate.Candidate
            {Id=Guid.NewGuid(), FirstName = "testName", LastName = "testName2", Email = "test@test.com", GovUkIdentifier = "" };
        //Act
        var actual = await repository.UpdateCandidateById(noCandidateExists);

        //Assert
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        Assert.IsNull(actual);
    }
}