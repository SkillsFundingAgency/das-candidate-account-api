using MediatR;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertTrainingCourse;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateTrainingCourse;
public class UpsertTrainingCourseCommandHandler(ITrainingCourseRespository trainingCourseRepository, IApplicationRepository applicationRepository) 
    : IRequestHandler<UpsertTrainingCourseCommand, UpsertTrainingCourseCommandResponse>
{
    public async Task<UpsertTrainingCourseCommandResponse> Handle(UpsertTrainingCourseCommand request, CancellationToken cancellationToken)
    {
        var application = await applicationRepository.GetById(request.ApplicationId);
        if (application == null || application.CandidateId != request.CandidateId)
        {
            throw new InvalidOperationException($"Application {request.ApplicationId} not found");
        }

        var result = await trainingCourseRepository.UpsertTrainingCourse(request.TrainingCourse, request.CandidateId);

        if (application.TrainingCoursesStatus == (short)SectionStatus.NotStarted)
        {
            application.TrainingCoursesStatus = (short)SectionStatus.InProgress;
            await applicationRepository.Update(application);
        }
        
        return new UpsertTrainingCourseCommandResponse
        {
            TrainingCourse = result.Item1,
            IsCreated = result.Item2
        };
    }
}
