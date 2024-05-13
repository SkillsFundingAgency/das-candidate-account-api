using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.AddLegacyApplication;

public class AddLegacyApplicationCommandHandler(IApplicationRepository applicationRepository) : IRequestHandler<AddLegacyApplicationCommand, AddLegacyApplicationCommandResponse>
{
    public async Task<AddLegacyApplicationCommandResponse> Handle(AddLegacyApplicationCommand request, CancellationToken cancellationToken)
    {
        var result = await applicationRepository.InsertLegacyApplication(request.LegacyApplication);

        return new AddLegacyApplicationCommandResponse
        {
            Id = result.Id
        };
    }
}