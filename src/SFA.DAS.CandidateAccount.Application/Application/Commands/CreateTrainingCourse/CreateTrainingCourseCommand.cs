using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.CreateTrainingCourse;
public class CreateTrainingCourseCommand : IRequest<CreateTrainingCourseCommandResponse>
{
    public Guid CandidateId { get; set; }
    public Guid ApplicationId { get; set; }
    public string CourseName { get; set; }
    public int YearAchieved { get; set; }
}
