using MediatR;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.CreateCandidate;

public class CreateCandidateCommandHandler(ICandidateRepository candidateRepository)
    : IRequestHandler<CreateCandidateCommand, CreateCandidateCommandResponse>
{

    public async Task<CreateCandidateCommandResponse> Handle(CreateCandidateCommand command, CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.Insert(new CandidateEntity
        {
            Id = command.Id,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            GovUkIdentifier = command.GovUkIdentifier,
            CreatedOn = DateTime.UtcNow,
            DateOfBirth = command.DateOfBirth
        });

        return new CreateCandidateCommandResponse
        {
            Candidate = candidate!
        };
    }
}