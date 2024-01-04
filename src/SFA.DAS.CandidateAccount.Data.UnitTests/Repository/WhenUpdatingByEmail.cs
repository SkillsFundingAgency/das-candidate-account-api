using Moq;
using SFA.DAS.CandidateAccount.Data.Repository;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using FluentAssertions;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository
{
    public class WhenUpdatingByEmail
    {
        private Mock<ICandidateAccountDataContext> _candidateAccountDataContext;
        private CandidateRepository _candidateRepository;
        private CandidateEntity _candidate;

        [SetUp]
        public void Arrange()
        {
            _candidate = new CandidateEntity
            {
                CreatedOn = DateTime.UtcNow,
                DateOfBirth = DateTime.UtcNow,
                Email = "testEmail",
                FirstName = "testFirstName",
                GovUkIdentifier = "testIdentifier",
                Id = new Guid(),
                LastName = "testLastName"
            };

            List<CandidateEntity> candidates = new List<CandidateEntity>{_candidate};

            _candidateAccountDataContext = new Mock<ICandidateAccountDataContext>();
            _candidateAccountDataContext.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates);
            _candidateRepository = new CandidateRepository(_candidateAccountDataContext.Object);
        }
        [Test]
        public async Task AndEmailExistsThenCandidateIsUpdated()
        {
            //Arrange
            var existingCandidate = new CandidateEntity
            {
                FirstName = "testName", LastName = "testName2", Email = "testEmail", GovUkIdentifier = "",
                MiddleNames = "testMiddleName", PhoneNumber = "123", UpdatedOn = DateTime.UtcNow,
                TermsOfUseAcceptedOn = DateTime.UtcNow
            };

            //Act
             await _candidateRepository.UpdateCandidateByEmail(existingCandidate);

            //Assert
            _candidateAccountDataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task AndEmailDoesNotExistThenNoUpdateIsMade()
        {
            //Arrange
            var noCandidateExists = new CandidateEntity
                { FirstName = "testName", LastName = "testName2", Email = "wrongEmail", GovUkIdentifier = "" };
            //Act
            await _candidateRepository.UpdateCandidateByEmail(noCandidateExists);

            //Assert
            _candidateAccountDataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
