using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateTrainingCourse;
public class UpsertTrainingCourseCommand : IRequest<UpsertTrainingCourseCommandResponse>
{
    public required Domain.Application.TrainingCourse TrainingCourse { get; set; }

}
