using MediatR;
using SFA.DAS.CandidateAccount.Data.EmploymentLocation;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertEmploymentLocation
{
    public class UpsertEmploymentLocationCommandHandler(IEmploymentLocationRepository employmentLocationRepository)
        : IRequestHandler<UpsertEmploymentLocationCommand, UpsertEmploymentLocationCommandResponse>
    {
        public async Task<UpsertEmploymentLocationCommandResponse> Handle(UpsertEmploymentLocationCommand command, CancellationToken cancellationToken)
        {
            var result = await employmentLocationRepository.UpsertEmploymentLocation(command.EmploymentLocation, command.CandidateId, cancellationToken);
            return new UpsertEmploymentLocationCommandResponse
            {
                EmploymentLocation = result.Item1,
                IsCreated = result.Item2
            };
        }
    }
}
