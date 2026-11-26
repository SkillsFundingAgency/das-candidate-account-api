using FluentAssertions;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application;

[TestFixture]
public class WhenGettingPagedApplications
{
    private static List<ApplicationEntity> BuildApplications(ApplicationStatus status, CandidateStatus candidateStatus, DateTime createdDate, int count = 3)
    {
        var applications = new List<ApplicationEntity>();
        for (var i = 0; i < count; i++)
        {
            var candidateId = Guid.NewGuid();
            applications.Add(new ApplicationEntity
            {
                Id = Guid.NewGuid(),
                CandidateId = candidateId,
                VacancyReference = $"VAC100000000{i}",
                Status = (short)status,
                CreatedDate = createdDate.AddDays(i),
                CandidateEntity = new CandidateEntity
                {
                    Id = candidateId,
                    Email = $"test{i}@example.com",
                    Status = (short)candidateStatus
                }
            });
        }
        return applications;
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Applications_Are_Returned_Oldest_First(
        [Frozen] Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        var applications = BuildApplications(ApplicationStatus.UnSuccessful, CandidateStatus.Completed, new DateTime(2026, 1, 1));
        applications.Reverse();
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(applications);

        var result = await repository.GetPagedApplications(null, 1, 1000, default);

        result.TotalCount.Should().Be(3);
        result.Items.Select(a => a.CreatedDate).Should().BeInAscendingOrder();
    }

    [Test]
    [RecursiveMoqInlineAutoData(ApplicationStatus.Submitted)]
    [RecursiveMoqInlineAutoData(ApplicationStatus.Withdrawn)]
    [RecursiveMoqInlineAutoData(ApplicationStatus.Successful)]
    [RecursiveMoqInlineAutoData(ApplicationStatus.UnSuccessful)]
    [RecursiveMoqInlineAutoData(ApplicationStatus.Expired)]
    public async Task Then_Submitted_Applications_Are_Returned_Whatever_Their_Outcome(
        ApplicationStatus status,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(BuildApplications(status, CandidateStatus.Completed, new DateTime(2026, 1, 1)));

        var result = await repository.GetPagedApplications(null, 1, 1000, default);

        result.Items.Should().HaveCount(3);
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Draft_Applications_Are_Not_Returned(
        [Frozen] Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(BuildApplications(ApplicationStatus.Draft, CandidateStatus.Completed, new DateTime(2026, 1, 1)));

        var result = await repository.GetPagedApplications(null, 1, 1000, default);

        result.Items.Should().BeEmpty();
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Applications_For_Deleted_Candidates_Are_Not_Returned(
        [Frozen] Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(BuildApplications(ApplicationStatus.UnSuccessful, CandidateStatus.Deleted, new DateTime(2026, 1, 1)));

        var result = await repository.GetPagedApplications(null, 1, 1000, default);

        result.Items.Should().BeEmpty();
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Applications_Are_Filtered_By_Added_After(
        [Frozen] Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(BuildApplications(ApplicationStatus.UnSuccessful, CandidateStatus.Completed, new DateTime(2026, 1, 1)));

        var result = await repository.GetPagedApplications(new DateTime(2026, 1, 2), 1, 1000, default);

        result.Items.Should().HaveCount(2);
        result.Items.Should().OnlyContain(a => a.CreatedDate >= new DateTime(2026, 1, 2));
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Results_Are_Paged(
        [Frozen] Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(BuildApplications(ApplicationStatus.UnSuccessful, CandidateStatus.Completed, new DateTime(2026, 1, 1)));

        var result = await repository.GetPagedApplications(null, 2, 2, default);

        result.Items.Should().ContainSingle();
        result.Items[0].CreatedDate.Should().Be(new DateTime(2026, 1, 3));
        result.TotalCount.Should().Be(3);
        result.TotalPages.Should().Be(2);
    }
}
