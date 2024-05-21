using MediatR;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;


namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteTrainingCourse
{
    public class DeleteTrainingCourseCommandHandler (ITrainingCourseRepository trainingCourseRepository) : IRequestHandler<DeleteTrainingCourseCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteTrainingCourseCommand command, CancellationToken cancellation)
        {
            await trainingCourseRepository.Delete(command.ApplicationId, command.Id, command.CandidateId);

            return Unit.Value;

        }
    }
}
