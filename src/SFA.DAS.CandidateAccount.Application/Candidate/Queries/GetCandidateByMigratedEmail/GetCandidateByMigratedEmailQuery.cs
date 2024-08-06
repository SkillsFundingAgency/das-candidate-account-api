using MediatR;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByMigratedId;
using SFA.DAS.CandidateAccount.Data.Candidate;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByMigratedEmail
{
    public class GetCandidateByMigratedEmailQuery : IRequest<GetCandidateByMigratedEmailQueryResult>
    {
        public string Email { get; set; }
    }

    public class GetCandidateByMigratedEmailQueryResult
    {
        public Domain.Candidate.Candidate? Candidate { get; set; }
    }

    public class GetCandidateByMigratedEmailQueryHandler(ICandidateRepository candidateRepository) : IRequestHandler<GetCandidateByMigratedEmailQuery, GetCandidateByMigratedEmailQueryResult>
    {
        public async Task<GetCandidateByMigratedEmailQueryResult> Handle(GetCandidateByMigratedEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await candidateRepository.GetByMigratedCandidateEmail(request.Email);

            if (result == null)
            {
                return new GetCandidateByMigratedEmailQueryResult
                {
                    Candidate = null
                };
            }

            return new GetCandidateByMigratedEmailQueryResult
            {
                Candidate = result
            };
        }
    }
 }
