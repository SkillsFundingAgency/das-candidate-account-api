namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class AddressEntity
    {
        public required Guid Id { get; init; }
        public string AddressLine1 { get; init; } = null!;
        public string? AddressLine2 { get; init; }
        public string Town { get; init; } = null!;
        public string? County { get; init; }
        public string Postcode { get; init; } = null!;
        public required Guid CandidateId { get; init; }
    }
}
