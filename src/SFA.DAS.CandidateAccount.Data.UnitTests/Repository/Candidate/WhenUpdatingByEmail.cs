using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Data.Repository;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate
{
    public class WhenUpdatingByEmail
    {
        [Test, MoqAutoData]
        public async Task AndEmailExistsThenCandidateIsUpdated(
            CandidateEntity candidate,
            [Frozen]Mock<ICandidateAccountDataContext> context,
            CandidateRepository repository)
        {
            //Arrange
            context.Setup(x => x.CandidateEntities)
                .ReturnsDbSet(new List<CandidateEntity> { candidate });
            var existingCandidate = new CandidateEntity
            {
                FirstName = "testName", LastName = "testName2", Email = candidate.Email, GovUkIdentifier = "",
                MiddleNames = "testMiddleName", PhoneNumber = "123", UpdatedOn = DateTime.UtcNow,
                TermsOfUseAcceptedOn = DateTime.UtcNow
            };

            //Act
             await repository.UpdateCandidateByEmail(existingCandidate);

            //Assert
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test, MoqAutoData]
        public async Task AndEmailDoesNotExistThenNoUpdateIsMade(
            CandidateEntity candidate,
            [Frozen]Mock<ICandidateAccountDataContext> context,
            CandidateRepository repository)
        {
            //Arrange
            context.Setup(x => x.CandidateEntities)
                .ReturnsDbSet(new List<CandidateEntity> { candidate });
            var noCandidateExists = new CandidateEntity
                { FirstName = "testName", LastName = "testName2", Email = "wrongEmail", GovUkIdentifier = "" };
            //Act
            await repository.UpdateCandidateByEmail(noCandidateExists);

            //Assert
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
