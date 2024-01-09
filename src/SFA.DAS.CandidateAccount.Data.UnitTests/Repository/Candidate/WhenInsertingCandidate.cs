using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Data.Repository;
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
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new List<CandidateEntity>());
            
        //Act
        await repository.Insert(candidate);

        //Assert
        context.Verify(x => x.CandidateEntities.AddAsync(candidate, CancellationToken.None), Times.Once);
        context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}