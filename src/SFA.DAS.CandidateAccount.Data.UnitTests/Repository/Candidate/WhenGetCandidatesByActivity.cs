using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate
{
    [TestFixture]
    public class WhenGetCandidatesByActivity
    {
        [Test, RecursiveMoqAutoData]
        public async Task And_Then_Candidates_Are_Returned(
            DateTime cutOffDateTime,
            List<CandidateEntity> candidates,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            CandidateRepository repository)
        {
            //Arrange
            foreach (var candidateEntity in candidates)
            {
                candidateEntity.Status = (short) CandidateStatus.Completed;
                candidateEntity.UpdatedOn = cutOffDateTime.AddMinutes(-1);
            }

            context.Setup(x => x.CandidateEntities).ReturnsDbSet( candidates);

            //Act
            var result = await repository.GetCandidatesByActivity(cutOffDateTime);

            //Assert
            result.Should().BeEquivalentTo(candidates);
        }

        [Test]
        [RecursiveMoqInlineAutoData(CandidateStatus.Deleted)]
        [RecursiveMoqInlineAutoData(CandidateStatus.InProgress)]
        [RecursiveMoqInlineAutoData(CandidateStatus.Incomplete)]
        public async Task And_Then_Status_Other_Than_Completed_Empty_Returned(
            CandidateStatus status,
            DateTime cutOffDateTime,
            List<CandidateEntity> candidates,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            CandidateRepository repository)
        {
            //Arrange
            foreach (var candidateEntity in candidates)
            {
                candidateEntity.Status = (short)status;
                candidateEntity.UpdatedOn = cutOffDateTime.AddMinutes(-1);
            }

            context.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates);

            //Act
            var result = await repository.GetCandidatesByActivity(cutOffDateTime);

            //Assert
            result.Should().BeEmpty();
        }

        [Test, RecursiveMoqInlineAutoData]
        public async Task And_Then_UpdatedOn_Greater_Than_CutOffDateTime_Empty_Returned(
            DateTime cutOffDateTime,
            List<CandidateEntity> candidates,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            CandidateRepository repository)
        {
            //Arrange
            foreach (var candidateEntity in candidates)
            {
                candidateEntity.Status = (short)CandidateStatus.Completed;
                candidateEntity.UpdatedOn = cutOffDateTime.AddMinutes(1);
            }

            context.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates);

            //Act
            var result = await repository.GetCandidatesByActivity(cutOffDateTime);

            //Assert
            result.Should().BeEmpty();
        }
    }
}