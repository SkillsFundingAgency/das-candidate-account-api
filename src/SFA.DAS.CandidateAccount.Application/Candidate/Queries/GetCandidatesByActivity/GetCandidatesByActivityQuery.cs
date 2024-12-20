using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByActivity
{
    public record GetCandidatesByActivityQuery(DateTime CutOffDateTime, int PageNumber, int PageSize) : IRequest<GetCandidatesByActivityQueryResult>;
}