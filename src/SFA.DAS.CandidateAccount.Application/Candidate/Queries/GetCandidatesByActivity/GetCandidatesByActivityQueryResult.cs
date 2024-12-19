namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByActivity
{
    public record GetCandidatesByActivityQueryResult
    {
        public List<Domain.Candidate.Candidate> Candidates { get; set; } = [];
    }
}