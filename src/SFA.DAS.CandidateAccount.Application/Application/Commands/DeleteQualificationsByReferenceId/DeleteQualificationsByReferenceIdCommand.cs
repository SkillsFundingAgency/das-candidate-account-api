using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteQualificationsByReferenceId;

public class DeleteQualificationsByReferenceIdCommand : IRequest<Unit>
{
    public Guid CandidateId { get; set; }
    public Guid ApplicationId { get; set; }
    public Guid QualificationReferenceId { get; set; }
}