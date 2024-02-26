using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertTrainingCourse;
public class UpsertTrainingCourseCommand : IRequest<UpsertTrainingCourseCommandResponse>
{
    public required Domain.Application.TrainingCourse TrainingCourse { get; set; }
    public required Guid CandidateId { get; set; }

}
