namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetAddress;

public record GetAddressQueryResult
{
    public Domain.Candidate.Address? Address { get; set; }
}