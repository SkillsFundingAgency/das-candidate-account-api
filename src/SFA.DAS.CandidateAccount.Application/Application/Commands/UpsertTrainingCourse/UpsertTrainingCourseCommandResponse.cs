namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateTrainingCourse;
public class UpsertTrainingCourseCommandResponse
{
    public required Domain.Application.TrainingCourse TrainingCourse { get; set; }
    public bool IsCreated { get; set; }
}
