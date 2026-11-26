using System.Text.Json;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetPagedApplications;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Models;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application;

[TestFixture]
public class WhenHandlingGetPagedApplicationsQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Applications_Are_Returned_From_The_Repository(
        GetPagedApplicationsQuery query,
        PaginatedList<ApplicationEntity> entities,
        [Frozen] Mock<IApplicationRepository> repository,
        GetPagedApplicationsQueryHandler handler)
    {
        repository.Setup(x => x.GetPagedApplications(query.AddedAfter, query.PageNumber, query.PageSize, CancellationToken.None))
            .ReturnsAsync(entities);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.TotalCount.Should().Be(entities.TotalCount);
        actual.PageIndex.Should().Be(entities.PageIndex);
        actual.PageSize.Should().Be(entities.PageSize);
        actual.TotalPages.Should().Be(entities.TotalPages);
        actual.Applications.Select(a => a.Id).Should().BeEquivalentTo(entities.Items.Select(a => a.Id));
        actual.Applications.Select(a => a.CandidateId).Should().BeEquivalentTo(entities.Items.Select(a => a.CandidateId));
    }

    [Test]
    public void Then_The_Status_Is_Serialised_As_Its_Name()
    {
        var application = new PagedApplication
        {
            Id = Guid.NewGuid(),
            VacancyReference = "VAC1000000001",
            Status = ApplicationStatus.UnSuccessful
        };

        var json = JsonSerializer.Serialize(application, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        json.Should().Contain("\"status\":\"UnSuccessful\"");
    }
}
