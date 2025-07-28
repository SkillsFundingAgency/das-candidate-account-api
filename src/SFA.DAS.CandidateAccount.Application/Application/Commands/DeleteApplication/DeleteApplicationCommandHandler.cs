using MediatR;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.EmploymentLocation;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Data.WorkExperience;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteApplication;

public class DeleteApplicationCommandHandler(
    IAdditionalQuestionRepository additionalQuestionRepository,
    IApplicationRepository applicationRepository,
    IQualificationRepository qualificationRepository,
    ITrainingCourseRepository trainingCourseRepository,
    IWorkHistoryRepository workHistoryRepository,
    IEmploymentLocationRepository employmentLocationRepository
    ): IRequestHandler<DeleteApplicationCommand, DeleteApplicationCommandResult>
{
    public async Task<DeleteApplicationCommandResult> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await applicationRepository.GetById(request.ApplicationId);
        if (application?.CandidateId != request.CandidateId)
        {
            return DeleteApplicationCommandResult.None;
        }

        await employmentLocationRepository.DeleteAllAsync(request.ApplicationId, request.CandidateId, cancellationToken);
        await workHistoryRepository.DeleteAllAsync(request.ApplicationId, request.CandidateId, cancellationToken);
        await trainingCourseRepository.DeleteAllAsync(request.ApplicationId, request.CandidateId, cancellationToken);
        await qualificationRepository.DeleteAllAsync(request.ApplicationId, request.CandidateId, cancellationToken);
        await additionalQuestionRepository.DeleteAllAsync(request.ApplicationId, request.CandidateId, cancellationToken);
        await applicationRepository.DeleteAsync(request.ApplicationId, request.CandidateId, cancellationToken);
        return new DeleteApplicationCommandResult(request.ApplicationId);
    }
}