using System.Text.Json.Serialization;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Models;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetPagedApplications;

public record GetPagedApplicationsQueryResult
{
    public List<PagedApplication> Applications { get; init; } = [];
    public int TotalCount { get; init; }
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }

    public static implicit operator GetPagedApplicationsQueryResult(PaginatedList<ApplicationEntity> source)
    {
        return new GetPagedApplicationsQueryResult
        {
            TotalCount = source.TotalCount,
            PageIndex = source.PageIndex,
            PageSize = source.PageSize,
            TotalPages = source.TotalPages,
            Applications = source.Items.Select(application => (PagedApplication)application).ToList()
        };
    }
}

public record PagedApplication
{
    public Guid Id { get; init; }
    public Guid CandidateId { get; init; }
    public string VacancyReference { get; init; } = null!;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ApplicationStatus Status { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime? SubmittedDate { get; init; }
    public DateTime? ResponseDate { get; init; }

    public static implicit operator PagedApplication(ApplicationEntity source)
    {
        return new PagedApplication
        {
            Id = source.Id,
            CandidateId = source.CandidateId,
            VacancyReference = source.VacancyReference,
            Status = (ApplicationStatus)source.Status,
            CreatedDate = source.CreatedDate,
            SubmittedDate = source.SubmittedDate,
            ResponseDate = source.ResponseDate
        };
    }
}
