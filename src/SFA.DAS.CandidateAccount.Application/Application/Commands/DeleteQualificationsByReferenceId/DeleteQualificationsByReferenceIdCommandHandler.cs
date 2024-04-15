using MediatR;
using SFA.DAS.CandidateAccount.Data.Qualification;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteQualificationsByReferenceId;

public class DeleteQualificationsByReferenceIdCommandHandler(IQualificationRepository qualificationRepository) : IRequestHandler<DeleteQualificationsByReferenceIdCommand, Unit>
{
    public async Task<Unit> Handle(DeleteQualificationsByReferenceIdCommand request, CancellationToken cancellationToken)
    {
        await qualificationRepository.DeleteCandidateApplicationQualificationsByReferenceId(request.CandidateId,
            request.ApplicationId, request.QualificationReferenceId);

        return new Unit();
    }
}