using SFA.DAS.CandidateAccount.Application.Application.Queries.GetAboutYouItem;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public class GetAboutYouItemApiResponse
{
    public string? SkillsAndStrengths { get; set; }
    public string? Improvements { get; set; }
    public string? HobbiesAndInterests { get; set; }
    public string? Support { get; set; }
    public Guid? ApplicationId { get; set; }

    public static implicit operator GetAboutYouItemApiResponse(GetAboutYouItemQueryResult source)
    {
        return new GetAboutYouItemApiResponse
        {
            SkillsAndStrengths = source.Strengths,
            Improvements = source.Improvements,
            HobbiesAndInterests = source.HobbiesAndInterests,
            Support = source.Support,
            ApplicationId = source.ApplicationId
        };
    }
}
