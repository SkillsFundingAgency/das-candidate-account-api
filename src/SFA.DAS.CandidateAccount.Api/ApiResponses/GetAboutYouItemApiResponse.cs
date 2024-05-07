using SFA.DAS.CandidateAccount.Application.Application.Queries.GetAboutYouItem;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public class GetAboutYouItemApiResponse
{
    public AboutYouItem? AboutYou { get; set; }

    public static implicit operator GetAboutYouItemApiResponse(Domain.Candidate.AboutYou source)
    {
        return new GetAboutYouItemApiResponse
        {
            AboutYou = source.ApplicationId is null ? null : (AboutYouItem)source
        };
    }
}

public class AboutYouItem
{
    public string? SkillsAndStrengths { get; set; }
    public string? Support { get; set; }
    public Guid? ApplicationId { get; set; }

    public static implicit operator AboutYouItem(Domain.Candidate.AboutYou source)
    {
        return new AboutYouItem
        {
            SkillsAndStrengths = source.Strengths,
            Support = source.Support,
            ApplicationId = source.ApplicationId
        };
    }
}

