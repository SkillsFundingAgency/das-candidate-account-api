using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourses;
public class GetTrainingCoursesQueryResult
{
    public IEnumerable<TrainingCourse> TrainingCourses { get; init; } = [];

    public static implicit operator GetTrainingCoursesQueryResult(List<TrainingCourseEntity> source)
    {
        return new GetTrainingCoursesQueryResult
        {
            TrainingCourses = source.Select(entity => (TrainingCourse)entity)
        };
    }
}
