using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertQualification;

public class UpsertQualificationCommand : IRequest<UpsertQualificationCommandResponse>
{
    public required Guid ApplicationId { get; set; }
    public required Domain.Application.Qualification Qualification { get; set; }
    public required Guid CandidateId { get; set; }
    public Guid QualificationReferenceId { get; set; }
}