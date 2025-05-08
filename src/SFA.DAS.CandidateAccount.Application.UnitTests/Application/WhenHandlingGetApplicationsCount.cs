using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsCount;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application
{
    [TestFixture]
    public class WhenHandlingGetApplicationsCount
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Applications_For_The_Candidate_Are_Returned(
            Guid candidateId,
            ApplicationStatus status,
            GetApplicationsCountQuery query,
            List<ApplicationEntity> entities,
            [Frozen] Mock<IApplicationRepository> repository,
            [Greedy] GetApplicationsCountQueryHandler handler)
        {
            query.CandidateId = candidateId;
            query.Statuses = [status];
            entities.ForEach(x => x.CandidateId = candidateId);
            entities.ForEach(x => x.Status = (short)status);

            repository.Setup(x => x.GetCountByStatus(query.CandidateId, query.Statuses.Select(applicationStatus => (short)applicationStatus).ToList(), CancellationToken.None)).ReturnsAsync(entities);

            var actual = await handler.Handle(query, CancellationToken.None);

            // Assert
            var result = actual.Stats;
            result.Count.Should().Be(entities.Count(x => x.Status == (short)status));
        }
    }
}
