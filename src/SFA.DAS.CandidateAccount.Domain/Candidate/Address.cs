namespace SFA.DAS.CandidateAccount.Domain.Candidate;

public record Address
{
    public required Guid Id { get; init; }
    public required string AddressLine1 { get; init; }
    public string? AddressLine2 { get; private init; }
    public string? AddressLine3 { get; private init; }
    public string? AddressLine4 { get; private init; }
    public required string Postcode { get; init; }
    public string? Uprn { get; private init; }
    public Guid CandidateId { get; private init; }

    public static implicit operator Address(AddressEntity source)
    {
        return new Address
        {
            Id = source.Id,
            AddressLine1 = source.AddressLine1,
            AddressLine2 = source.AddressLine2,
            AddressLine3 = source.AddressLine3,
            AddressLine4 = source.AddressLine4,
            Postcode = source.Postcode,
            Uprn = source.Uprn,
            CandidateId = source.CandidateId,
        };
    }
}