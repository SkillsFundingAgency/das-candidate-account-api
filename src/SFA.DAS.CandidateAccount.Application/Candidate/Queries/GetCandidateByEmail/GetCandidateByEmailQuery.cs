using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByEmail;

public class GetCandidateByEmailQuery : IRequest<GetCandidateByEmailQueryResult>
{
    public string Email { get; set; }
}