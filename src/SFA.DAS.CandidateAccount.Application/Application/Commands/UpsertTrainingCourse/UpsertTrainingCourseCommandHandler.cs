using MediatR;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateTrainingCourse;
public class UpsertTrainingCourseCommandHandler(ITrainingCourseRespository TrainingCourseRepository) 
    : IRequestHandler<UpsertTrainingCourseCommand, UpsertTrainingCourseCommandResponse>
{
    public async Task<UpsertTrainingCourseCommandResponse> Handle(UpsertTrainingCourseCommand request, CancellationToken cancellationToken)
    {
        var result = await TrainingCourseRepository.UpsertTrainingCourse(request.TrainingCourse);
        return new UpsertTrainingCourseCommandResponse
        {
            TrainingCourse = result.Item1,
            IsCreated = result.Item2
        };
    }
}
