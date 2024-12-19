using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByActivity;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Candidate
{
    [TestFixture]
    public class WhenHandlingGetCandidatesByActivityQuery
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Candidates_Found_By_Activity_And_Returned(
            DateTime cutOffDateTime,
            GetCandidatesByActivityQuery query,
            List<CandidateEntity> entities,
            [Frozen] Mock<ICandidateRepository> repository,
            GetCandidatesByActivityQueryHandler handler)
        {
            query = new GetCandidatesByActivityQuery(CutOffDateTime: cutOffDateTime);
            foreach (var candidateEntity in entities)
            {
                candidateEntity.Status = (short)CandidateStatus.Completed;
                candidateEntity.UpdatedOn = cutOffDateTime.AddMinutes(-1);
            }
            repository.Setup(x => x.GetCandidatesByActivity(It.IsAny<DateTime>())).ReturnsAsync(entities);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Candidates.Count.Should().Be(entities.Count);
            actual.Candidates.Should().BeEquivalentTo(entities.Select(x => (Domain.Candidate.Candidate)x!));
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Candidates_NotFound_By_Activity_And_Empty_Returned(
            GetCandidatesByActivityQuery query,
            [Frozen] Mock<ICandidateRepository> repository,
            GetCandidatesByActivityQueryHandler handler)
        {
            repository.Setup(x => x.GetCandidatesByActivity(It.IsAny<DateTime>())).ReturnsAsync(new List<CandidateEntity>{ Capacity = 0});

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Candidates.Count.Should().Be(0);
        }
    }
}
