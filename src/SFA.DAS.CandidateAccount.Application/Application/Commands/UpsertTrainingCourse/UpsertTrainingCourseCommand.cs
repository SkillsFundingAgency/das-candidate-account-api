using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateTrainingCourse;
public class UpsertTrainingCourseCommand : IRequest<UpsertTrainingCourseCommandResponse>
{
    public required Guid ApplicationId { get; set; }
    public required Domain.Application.TrainingCourse TrainingCourse { get; set; }
    public required Guid CandidateId { get; set; }

}
