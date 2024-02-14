using SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourseItem;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public class GetTrainingCourseItemApiResponse
{
    public Guid Id { get; set; }
    public Guid ApplicationId { get; set; }
    public string CourseName { get; set; }
    public int YearAchieved { get; set; }

    public static implicit operator GetTrainingCourseItemApiResponse(GetTrainingCourseItemQueryResult source)
    {
        return new GetTrainingCourseItemApiResponse
        {
            Id = source.Id,
            ApplicationId = source.ApplicationId,
            CourseName = source.Title,
            YearAchieved = source.ToYear
        };
    }
}
