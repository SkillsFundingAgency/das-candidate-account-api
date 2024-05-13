using MediatR;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.AddLegacyApplication
{
    public class AddLegacyApplicationCommand : IRequest<AddLegacyApplicationCommandResponse>
    {
        public LegacyApplication LegacyApplication { get; set; } = null!;
    }

    public class AddLegacyApplicationCommandResponse
    {
        public Guid Id { get; set; }
    }

    public class AddLegacyApplicationCommandHandler : IRequestHandler<AddLegacyApplicationCommand, AddLegacyApplicationCommandResponse>
    {
        public Task<AddLegacyApplicationCommandResponse> Handle(AddLegacyApplicationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
