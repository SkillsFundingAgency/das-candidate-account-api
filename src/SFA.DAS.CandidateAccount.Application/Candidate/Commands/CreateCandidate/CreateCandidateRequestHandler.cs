using MediatR;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.CreateCandidate;

public class CreateCandidateRequestHandler(ICandidateRepository candidateRepository)
    : IRequestHandler<CreateCandidateRequest, CreateCandidateResponse>
{

    public async Task<CreateCandidateResponse> Handle(CreateCandidateRequest request, CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.Insert(new CandidateEntity
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            GovUkIdentifier = request.GovUkIdentifier,
            CreatedOn = DateTime.UtcNow
        });

        return new CreateCandidateResponse
        {
            Candidate = candidate
        };
    }
}