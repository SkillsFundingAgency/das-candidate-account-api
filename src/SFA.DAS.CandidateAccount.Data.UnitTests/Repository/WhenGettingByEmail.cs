using Moq;
using SFA.DAS.CandidateAccount.Data.Repository;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository
{
    public class WhenGettingByEmail
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

        [Ignore("Todo")]
        public async Task AndEmailExistsThenCandidateIsReturned()
        {
            //Arrange

            _candidateAccountDataContext.Setup(x => x.CandidateEntities.FirstOrDefaultAsync(CancellationToken.None)).ReturnsAsync(_candidate);

            //Act
            var result = await _candidateRepository.GetCandidateByEmail(_candidate.Email);

            //Assert
            result.Should().BeEquivalentTo(_candidate);
        }

        [Test]
        public async Task AndEmailDoesNotExistThenReturnsNull()
        {
            //Arrange

            //Act
            var result = await _candidateRepository.GetCandidateByEmail("wrongEmail");

            //Assert
            result.Should().BeNull();
        }
    }
}
