using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SFA.DAS.CandidateAccount.Data.Repository;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository
{
    public class WhenInsertingCandidate
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

            List<CandidateEntity> candidates = new List<CandidateEntity>();

            _candidateAccountDataContext = new Mock<ICandidateAccountDataContext>();
            _candidateAccountDataContext.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates);
            _candidateRepository = new CandidateRepository(_candidateAccountDataContext.Object);
        }

        [Test]
        public async Task ThenTheCandidateIsInserted()
        {
            //Arrange
            
            //Act
            await _candidateRepository.Insert(_candidate);

            //Assert
            _candidateAccountDataContext.Verify(x => x.CandidateEntities.AddAsync(_candidate, CancellationToken.None), Times.Once);
            _candidateAccountDataContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }       
}
