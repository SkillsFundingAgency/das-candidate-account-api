using FluentAssertions;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate;

[TestFixture]
public class WhenGettingPagedCandidates
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Candidates_Added_On_Or_After_The_Date_Are_Returned(
        DateTime addedSince,
        List<CandidateEntity> candidates,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        foreach (var candidate in candidates)
        {
            candidate.Status = (short)CandidateStatus.Completed;
            candidate.CreatedOn = addedSince.AddDays(1);
        }
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates);

        var result = await repository.GetPagedCandidates(addedSince, 1, 1000, default);

        result.Items.Should().BeEquivalentTo(candidates);
        result.TotalCount.Should().Be(candidates.Count);
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Candidates_Added_Before_The_Date_Are_Not_Returned(
        DateTime addedSince,
        List<CandidateEntity> candidates,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        foreach (var candidate in candidates)
        {
            candidate.Status = (short)CandidateStatus.Completed;
            candidate.CreatedOn = addedSince.AddDays(-1);
        }
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates);

        var result = await repository.GetPagedCandidates(addedSince, 1, 1000, default);

        result.Items.Should().BeEmpty();
    }

    [Test]
    [RecursiveMoqInlineAutoData(ApplicationStatus.Draft)]
    [RecursiveMoqInlineAutoData(ApplicationStatus.Successful)]
    [RecursiveMoqInlineAutoData(ApplicationStatus.UnSuccessful)]
    public async Task Then_Candidates_Are_Returned_Whatever_Their_Applications(
        ApplicationStatus status,
        List<CandidateEntity> candidates,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        foreach (var candidate in candidates)
        {
            candidate.Status = (short)CandidateStatus.Completed;
            candidate.Applications = new List<ApplicationEntity>
            {
                new() { Id = Guid.NewGuid(), CandidateId = candidate.Id, VacancyReference = "VAC1000000001", Status = (short)status }
            };
        }
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates);

        var result = await repository.GetPagedCandidates(null, 1, 1000, default);

        result.Items.Should().HaveCount(candidates.Count);
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Candidates_With_No_Applications_Are_Returned(
        List<CandidateEntity> candidates,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        foreach (var candidate in candidates)
        {
            candidate.Status = (short)CandidateStatus.Completed;
            candidate.Applications = new List<ApplicationEntity>();
        }
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates);

        var result = await repository.GetPagedCandidates(null, 1, 1000, default);

        result.Items.Should().HaveCount(candidates.Count);
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Deleted_Candidates_Are_Not_Returned(
        List<CandidateEntity> candidates,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        foreach (var candidate in candidates)
        {
            candidate.Status = (short)CandidateStatus.Deleted;
        }
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates);

        var result = await repository.GetPagedCandidates(null, 1, 1000, default);

        result.Items.Should().BeEmpty();
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Results_Are_Paged_Oldest_First(
        List<CandidateEntity> candidates,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        CandidateRepository repository)
    {
        var baseDate = new DateTime(2026, 1, 1);
        for (var i = 0; i < candidates.Count; i++)
        {
            candidates[i].Status = (short)CandidateStatus.Completed;
            candidates[i].CreatedOn = baseDate.AddDays(candidates.Count - i);
        }
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(candidates);

        var result = await repository.GetPagedCandidates(null, 1, 1, default);

        result.Items.Should().ContainSingle();
        result.Items[0].Id.Should().Be(candidates.OrderBy(c => c.CreatedOn).First().Id);
        result.TotalCount.Should().Be(candidates.Count);
        result.TotalPages.Should().Be(candidates.Count);
    }
}
