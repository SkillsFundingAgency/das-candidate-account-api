using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetPagedCandidates;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.CandidateAccount.Domain.Models;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Candidate;

[TestFixture]
public class WhenHandlingGetPagedCandidatesQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Candidates_Are_Returned_From_The_Repository(
        GetPagedCandidatesQuery query,
        PaginatedList<CandidateEntity> entities,
        [Frozen] Mock<ICandidateRepository> repository,
        GetPagedCandidatesQueryHandler handler)
    {
        repository.Setup(x => x.GetPagedCandidates(query.AddedSince, query.PageNumber, query.PageSize, CancellationToken.None))
            .ReturnsAsync(entities);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.TotalCount.Should().Be(entities.TotalCount);
        actual.PageIndex.Should().Be(entities.PageIndex);
        actual.PageSize.Should().Be(entities.PageSize);
        actual.TotalPages.Should().Be(entities.TotalPages);
        actual.Candidates.Select(c => c.Id).Should().BeEquivalentTo(entities.Items.Select(c => c.Id));
    }

    [Test, RecursiveMoqAutoData]
    public void Then_The_Candidate_Is_Mapped(CandidateEntity candidate)
    {
        var actual = (PagedCandidate)candidate;

        actual.Id.Should().Be(candidate.Id);
        actual.FirstName.Should().Be(candidate.FirstName);
        actual.LastName.Should().Be(candidate.LastName);
        actual.Email.Should().Be(candidate.Email);
        actual.PhoneNumber.Should().Be(candidate.PhoneNumber);
        actual.DateOfBirth.Should().Be(candidate.DateOfBirth);
        actual.CreatedOn.Should().Be(candidate.CreatedOn);
    }
}
