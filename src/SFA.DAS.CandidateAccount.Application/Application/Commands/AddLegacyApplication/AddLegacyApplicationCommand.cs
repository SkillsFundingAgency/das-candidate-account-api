using MediatR;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.AddLegacyApplication
{
    public class AddLegacyApplicationCommand : IRequest<AddLegacyApplicationCommandResponse>
    {
        public LegacyApplication LegacyApplication { get; set; } = null!;
    }
}
