using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.CandidateAccount.Domain.Models;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetPagedCandidates;

public record GetPagedCandidatesQueryResult
{
    public List<PagedCandidate> Candidates { get; init; } = [];
    public int TotalCount { get; init; }
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }

    public static implicit operator GetPagedCandidatesQueryResult(PaginatedList<CandidateEntity> source)
    {
        return new GetPagedCandidatesQueryResult
        {
            TotalCount = source.TotalCount,
            PageIndex = source.PageIndex,
            PageSize = source.PageSize,
            TotalPages = source.TotalPages,
            Candidates = source.Items.Select(candidate => (PagedCandidate)candidate).ToList()
        };
    }
}

public record PagedCandidate
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public DateTime CreatedOn { get; init; }

    public static implicit operator PagedCandidate(CandidateEntity source)
    {
        return new PagedCandidate
        {
            Id = source.Id,
            FirstName = source.FirstName,
            LastName = source.LastName,
            Email = source.Email,
            PhoneNumber = source.PhoneNumber,
            DateOfBirth = source.DateOfBirth,
            CreatedOn = source.CreatedOn
        };
    }
}
