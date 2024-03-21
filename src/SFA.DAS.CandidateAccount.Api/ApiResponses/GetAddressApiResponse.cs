using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetAddress;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public record GetAddressApiResponse
{
    public Guid Id { get; set; }
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
    public string Town { get; set; } = null!;
    public string? County { get; set; }
    public string? Postcode { get; set; }
    public Guid CandidateId { get; set; }

    public static implicit operator GetAddressApiResponse(GetAddressQueryResult source)
    {
        if (source.Address is null) return null!;

        return new GetAddressApiResponse
        {
            Id = source.Address.Id,
            AddressLine1 = source.Address.AddressLine1,
            AddressLine2 = source.Address.AddressLine2,
            Town = source.Address.Town,
            County = source.Address.County,
            Postcode = source.Address.Postcode,
            CandidateId = source.Address.CandidateId,
        };
    }
}