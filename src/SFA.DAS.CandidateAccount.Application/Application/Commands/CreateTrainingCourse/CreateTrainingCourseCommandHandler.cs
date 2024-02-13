using MediatR;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.CreateTrainingCourse;
public class CreateTrainingCourseCommandHandler(ITrainingCourseRespository TrainingCourseRepository)
    : IRequestHandler<CreateTrainingCourseCommand, CreateTrainingCourseCommandResponse>
{
    public async Task<CreateTrainingCourseCommandResponse> Handle(CreateTrainingCourseCommand request, CancellationToken cancellationToken)
    {
        var result = await TrainingCourseRepository.Insert(new TrainingCourseEntity
        {
            ApplicationId = request.ApplicationId,
            Title = request.CourseName,
            ToYear = request.YearAchieved
        });

        return new CreateTrainingCourseCommandResponse
        {
            TrainingCourseId = result.Id,
            TrainingCourse = result
        };
    }
}
