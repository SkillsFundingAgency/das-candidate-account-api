using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.CandidateAccount.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate
{
    [TestFixture]
    public class WhenGetCandidatesByActivity
    {
        [Test, RecursiveMoqAutoData]
        public async Task And_Then_Candidates_Are_Returned(
            int pageNumber,
            int pageSize,
            DateTime cutOffDateTime,
            PaginatedList<CandidateEntity> candidates,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            CandidateRepository repository)
        {
            //Arrange
            foreach (var candidateEntity in candidates.Items)
            {
                candidateEntity.Status = (short) CandidateStatus.Completed;
                candidateEntity.UpdatedOn = cutOffDateTime.AddMinutes(-1);
            }

            context.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates.Items);

            //Act
            var result = await repository.GetCandidatesByActivity(cutOffDateTime, 1, 1000, default);

            //Assert
            result.Items.Should().BeEquivalentTo(candidates.Items);
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
            var result = await repository.GetCandidatesByActivity(cutOffDateTime, 1, 1000, default);

            //Assert
            result.Items.Should().BeEmpty();
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
            var result = await repository.GetCandidatesByActivity(cutOffDateTime, 1, 1000, default);

            //Assert
            result.Items.Should().BeEmpty();
        }
    }
}