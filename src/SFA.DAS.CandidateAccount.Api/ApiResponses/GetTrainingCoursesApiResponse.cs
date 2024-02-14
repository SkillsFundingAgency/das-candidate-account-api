using SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourses;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses
{
    public class GetTrainingCoursesApiResponse
    {
        public IEnumerable<TrainingCourseItem> TrainingCourses { get; init; } = [];

        public static implicit operator GetTrainingCoursesApiResponse(GetTrainingCoursesQueryResult source)
        {
            return new GetTrainingCoursesApiResponse
            {
                TrainingCourses = source.TrainingCourses.Select(entity => (TrainingCourseItem)entity)
            };
        }
    }

    public class TrainingCourseItem
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public string CourseName { get; set; }
        public int YearAchieved { get; set; }

        public static implicit operator TrainingCourseItem(GetTrainingCoursesQueryResult.TrainingCourseItem source)
        {
            return new TrainingCourseItem
            {
                Id = source.Id,
                ApplicationId = source.ApplicationId,
                CourseName = source.Title,
                YearAchieved = source.ToYear
            };
        }
    }
}
