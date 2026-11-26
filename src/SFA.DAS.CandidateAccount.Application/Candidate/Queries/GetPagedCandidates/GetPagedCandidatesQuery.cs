using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetPagedCandidates;

public record GetPagedCandidatesQuery(
    DateTime? AddedSince,
    int PageNumber,
    int PageSize) : IRequest<GetPagedCandidatesQueryResult>;
