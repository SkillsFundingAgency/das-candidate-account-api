using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.UpdateCandidate;

public class UpdateCandidateCommand : IRequest<UpdateCandidateCommandResponse>
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}