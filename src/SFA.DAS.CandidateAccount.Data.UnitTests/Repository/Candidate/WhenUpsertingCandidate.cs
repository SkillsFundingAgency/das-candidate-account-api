using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate;

public class WhenUpsertingCandidate
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
        var actual = await repository.UpsertCandidate(existingCandidate);

        //Assert
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        actual.Item1.FirstName.Should().Be(existingCandidate.FirstName);
        actual.Item1.LastName.Should().Be(existingCandidate.LastName);
        actual.Item1.GovUkIdentifier.Should().Be(candidate.GovUkIdentifier);
        actual.Item1.DateOfBirth.Should().Be(candidate.DateOfBirth);
        actual.Item1.UpdatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        actual.Item2.Should().BeFalse();
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
        existingCandidate.DateOfBirth = null;

        //Act
        var actual = await repository.UpsertCandidate(existingCandidate);

        //Assert
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        actual.Item1.FirstName.Should().Be(candidate.FirstName);
        actual.Item1.LastName.Should().Be(candidate.LastName);
        actual.Item1.Email.Should().Be(existingCandidate.Email);
        actual.Item1.GovUkIdentifier.Should().Be(candidate.GovUkIdentifier);
        actual.Item1.DateOfBirth.Should().Be(candidate.DateOfBirth);
        actual.Item1.PhoneNumber.Should().Be(existingCandidate.PhoneNumber);
        actual.Item1.MigratedEmail.Should().Be(existingCandidate.MigratedEmail);
        actual.Item1.UpdatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Test, RecursiveMoqAutoData]
    public async Task AndIdDoesNotExistThenNoUpdateIsMadeAndInserted(
        CandidateEntity candidate,
        CandidateStatus status,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        candidate.Status = (short)status;
        context.Setup(x => x.CandidateEntities)
            .ReturnsDbSet(new List<CandidateEntity> { candidate });
        var noCandidateExists = new Domain.Candidate.Candidate
        {
            Id=Guid.NewGuid(), FirstName = "testName", LastName = "testName2", Email = "test@test.com", GovUkIdentifier = "someidentifier", Status = CandidateStatus.Incomplete
        };
        
        //Act
        var actual = await repository.UpsertCandidate(noCandidateExists);

        //Assert
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsNotNull(actual);
        actual.Item2.Should().BeTrue();
        actual.Item1.Should().BeEquivalentTo(noCandidateExists, options => options.Excluding(c=>c.CreatedOn).Excluding(c=>c.UpdatedOn).Excluding(c => c.Status));
        actual.Item1.Status.Should().Be((short)CandidateStatus.Incomplete);
        actual.Item1.CreatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }
}