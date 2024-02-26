namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertTrainingCourse;
public class UpsertTrainingCourseCommandResponse
{
    public required Domain.Application.TrainingCourse TrainingCourse { get; set; }
    public bool IsCreated { get; set; }
}
