namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.DeleteCandidate
{
    public record DeleteCandidateCommandResult
    {
        public Domain.Candidate.Candidate? Candidate { get; set; }
    }
}