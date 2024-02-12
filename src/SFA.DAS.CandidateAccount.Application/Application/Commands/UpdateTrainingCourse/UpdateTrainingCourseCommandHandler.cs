using MediatR;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateTrainingCourse;
public class UpdateTrainingCourseCommandHandler(ITrainingCourseRespository TrainingCourseRepository) : IRequestHandler<UpdateTrainingCourseCommand>
{
    public async Task Handle(UpdateTrainingCourseCommand request, CancellationToken cancellationToken)
    {
        await TrainingCourseRepository.Update(new TrainingCourseEntity
        {
            Id = request.Id,
            ApplicationId = request.ApplicationId,
            Title = request.CourseName,
            ToYear = request.YearAchieved
        });
    }
}
