namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.UpsertCandidate;

public class UpsertCandidateCommandResponse
{
    public required Domain.Candidate.Candidate Candidate { get; set; }
    public bool IsCreated { get; set; }
}