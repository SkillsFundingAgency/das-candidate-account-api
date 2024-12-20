using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByActivity;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.CandidateAccount.Domain.Models;
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
            PaginatedList<CandidateEntity> entities,
            [Frozen] Mock<ICandidateRepository> repository,
            GetCandidatesByActivityQueryHandler handler)
        {
            query = new GetCandidatesByActivityQuery(CutOffDateTime: cutOffDateTime, query.PageNumber, query.PageSize);
            foreach (var candidateEntity in entities.Items)
            {
                candidateEntity.Status = (short)CandidateStatus.Completed;
                candidateEntity.UpdatedOn = cutOffDateTime.AddMinutes(-1);
            }
            repository.Setup(x => x.GetCandidatesByActivity(query.CutOffDateTime, query.PageNumber, query.PageSize, CancellationToken.None)).ReturnsAsync(entities);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Candidates.Count.Should().Be(entities.Items.Count);
            actual.Candidates.Should().BeEquivalentTo(entities.Items.Select(x => (Domain.Candidate.Candidate)x!));
        }
    }
}