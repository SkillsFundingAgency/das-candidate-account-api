namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetAboutYouItem;
public class GetAboutYouItemQueryResult
{
    public string? Strengths { get; set; }
    public string? Improvements { get; set; }
    public string? HobbiesAndInterests { get; set; }
    public string? Support { get; set; }
    public Guid? ApplicationId { get; set; }

    public static implicit operator GetAboutYouItemQueryResult(Domain.Candidate.AboutYou source)
    {
        return new GetAboutYouItemQueryResult
        {
            Strengths = source.Strengths,
            Improvements = source.Improvements,
            HobbiesAndInterests = source.HobbiesAndInterests,
            Support = source.Support,
            ApplicationId = source.ApplicationId
        };
    }
}
