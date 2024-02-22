using Newtonsoft.Json;

namespace SFA.DAS.CandidateAccount.Api.ApiRequests;

public class PutAboutYouItemRequest
{
    [JsonProperty("skillsAndStrengths")]
    public string? Strengths { get; set; }
    public string? Improvements { get; set; }
    public string? HobbiesAndInterests { get; set; }
    public string? Support { get; set; }
}
