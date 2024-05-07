namespace SFA.DAS.CandidateAccount.Domain.Candidate;

public record Address
{
    public required Guid Id { get; init; }
    public string? Uprn { get; set; }
    public required string AddressLine1 { get; init; }
    public string? AddressLine2 { get; init; }
    public required string Town { get; init; }
    public string? County { get; init; }
    public required string Postcode { get; init; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public Guid CandidateId { get; init; }

    public static implicit operator Address?(AddressEntity? source)
    {
        if (source == null)
        {
            return null;
        }
        return new Address
        {
            Id = source.Id,
            Uprn = source.Uprn,
            AddressLine1 = source.AddressLine1,
            AddressLine2 = source.AddressLine2,
            Town = source.Town,
            County = source.County,
            Postcode = source.Postcode,
            Latitude = source.Latitude,
            Longitude = source.Longitude,
            CandidateId = source.CandidateId,
        };
    }
}