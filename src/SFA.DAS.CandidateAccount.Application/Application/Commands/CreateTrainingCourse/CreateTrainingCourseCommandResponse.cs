using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.CreateTrainingCourse;
public class CreateTrainingCourseCommandResponse
{
    public Guid TrainingCourseId { get; set; }
    public TrainingCourseEntity TrainingCourse { get; set; }
}
