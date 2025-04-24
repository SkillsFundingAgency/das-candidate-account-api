using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertEmploymentLocation
{
    public record UpsertEmploymentLocationCommand : IRequest<UpsertEmploymentLocationCommandResponse>
    {
        public required Domain.Application.EmploymentLocation EmploymentLocation { get; init; }
        public Guid CandidateId { get; init; }
    }
}