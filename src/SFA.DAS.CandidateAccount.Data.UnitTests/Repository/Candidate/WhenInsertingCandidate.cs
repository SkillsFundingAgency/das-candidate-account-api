using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate;

public class WhenInsertingCandidate
{
    [Test, RecursiveMoqAutoData]
    public async Task ThenTheCandidateIsInserted(
        CandidateEntity candidate,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new List<CandidateEntity>()
        {
            Capacity = 0
        });
            
        //Act
        var actual = await repository.Insert(candidate);

        //Assert
        actual.Item1.Should().Be(candidate);
        context.Verify(x => x.CandidateEntities.AddAsync(candidate, CancellationToken.None), Times.Once);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_TheCandidate_Exist_Same_Returned(
        CandidateEntity candidate,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        //Arrange
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new List<CandidateEntity>()
        {
            candidate
        });

        //Act
        var actual = await repository.Insert(candidate);

        //Assert
        actual.Item1.Should().Be(candidate);
        context.Verify(x => x.CandidateEntities.AddAsync(candidate, CancellationToken.None), Times.Never);
        context.Verify(x => x.CandidateEntities.Update(candidate), Times.Once);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}