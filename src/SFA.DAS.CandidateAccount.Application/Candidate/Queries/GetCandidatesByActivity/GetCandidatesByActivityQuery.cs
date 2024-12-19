using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByActivity
{
    public record GetCandidatesByActivityQuery(DateTime CutOffDateTime) : IRequest<GetCandidatesByActivityQueryResult>;
}