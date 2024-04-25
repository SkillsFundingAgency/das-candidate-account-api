using MediatR;
using SFA.DAS.CandidateAccount.Data.Qualification;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteQualification;

public class DeleteQualificationCommandHandler(IQualificationRepository qualificationRepository) : IRequestHandler<DeleteQualificationCommand, Unit>
{
    public async Task<Unit> Handle(DeleteQualificationCommand request, CancellationToken cancellationToken)
    {
        await qualificationRepository.DeleteCandidateApplicationQualificationById(request.CandidateId,
            request.ApplicationId, request.Id);

        return new Unit();
    }
}