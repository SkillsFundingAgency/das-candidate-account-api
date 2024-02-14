using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourses;
public class GetTrainingCoursesQueryResult
{
    public IEnumerable<TrainingCourseItem> TrainingCourses { get; init; } = [];

    public static implicit operator GetTrainingCoursesQueryResult(List<TrainingCourseEntity> source)
    {
        return new GetTrainingCoursesQueryResult
        {
            TrainingCourses = source.Select(entity => (TrainingCourseItem)entity)
        };
    }

    public class TrainingCourseItem
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public string Title { get; set; }
        public int ToYear { get; set; }

        public static implicit operator TrainingCourseItem(TrainingCourseEntity source)
        {
            return new TrainingCourseItem
            {
                Id = source.Id,
                ApplicationId = source.ApplicationId,
                Title = source.Title,
                ToYear = source.ToYear
            };
        }
    }
}
