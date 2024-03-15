using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetAddress;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public record GetAddressApiResponse
{
    public Guid Id { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string? Postcode { get; set; }
    public string? Uprn { get; set; }
    public Guid CandidateId { get; set; }

    public static implicit operator GetAddressApiResponse(GetAddressQueryResult source)
    {
        if (source.Address is null) return null!;

        return new GetAddressApiResponse
        {
            Id = source.Address.Id,
            AddressLine1 = source.Address.AddressLine1,
            AddressLine2 = source.Address.AddressLine2,
            AddressLine3 = source.Address.AddressLine3,
            AddressLine4 = source.Address.AddressLine4,
            Postcode = source.Address.Postcode,
            Uprn = source.Address.Uprn,
            CandidateId = source.Address.CandidateId,
        };
    }
}