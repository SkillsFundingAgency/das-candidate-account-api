using MediatR;
using SFA.DAS.CandidateAccount.Data.Candidate;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.UpsertCandidate;

public class UpsertCandidateCommandHandler(ICandidateRepository candidateRepository) : IRequestHandler<UpsertCandidateCommand, UpsertCandidateCommandResponse>
{
    public async Task<UpsertCandidateCommandResponse> Handle(UpsertCandidateCommand request, CancellationToken cancellationToken)
    {
        var result = await candidateRepository.UpsertCandidate(request.Candidate);

        return new UpsertCandidateCommandResponse
        {
            Candidate = result.Item1,
            IsCreated = result.Item2
        };
    }
}