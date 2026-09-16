using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByEmail;

public record GetCandidateByEmailQuery(string EmailAddress) : IRequest<GetCandidateByEmailQueryResult>;