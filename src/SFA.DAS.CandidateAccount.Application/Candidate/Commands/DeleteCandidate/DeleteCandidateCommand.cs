using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.DeleteCandidate
{
    public record DeleteCandidateCommand(Guid CandidateId) : IRequest<DeleteCandidateCommandResult>;
}
