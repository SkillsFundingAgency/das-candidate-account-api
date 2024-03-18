namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class AddressEntity
    {
        public required Guid Id { get; init; }
        public string AddressLine1 { get; init; } = null!;
        public string? AddressLine2 { get; init; }
        public string? AddressLine3 { get; init;}
        public string? AddressLine4 { get; init; }
        public string Postcode { get; init; } = null!;
        public string? Uprn { get; init; }
        public required Guid CandidateId { get; init; }
    }
}
