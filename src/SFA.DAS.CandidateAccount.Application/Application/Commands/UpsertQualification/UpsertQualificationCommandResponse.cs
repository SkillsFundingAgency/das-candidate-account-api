namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertQualification;

public class UpsertQualificationCommandResponse
{
    public required Domain.Application.Qualification Qualification { get; set; }
    public bool IsCreated { get; set; }
}