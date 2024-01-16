using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.CreateCandidate;

public class CreateCandidateRequest : IRequest<CreateCandidateResponse>
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string GovUkIdentifier { get; set; }
    public Guid Id { get; set; }
    public DateTime DateOfBirth { get; set; }
}