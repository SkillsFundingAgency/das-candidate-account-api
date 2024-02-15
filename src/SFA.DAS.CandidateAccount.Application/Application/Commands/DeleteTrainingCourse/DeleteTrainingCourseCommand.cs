using MediatR;


namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteTrainingCourse
{
    public class DeleteTrainingCourseCommand : IRequest<Unit>
    {       
        public Guid TrainingCourseid { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid CandidateId { get; set; }
    }
}
