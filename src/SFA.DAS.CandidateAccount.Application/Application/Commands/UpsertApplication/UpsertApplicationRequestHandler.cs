using System.ComponentModel.DataAnnotations;
using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Application;
using ValidationResult = SFA.DAS.CandidateAccount.Domain.RequestHandlers.ValidationResult;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;

public class UpsertApplicationRequestHandler(
    ICandidateRepository candidateRepository,
    IApplicationRepository applicationRepository)
    : IRequestHandler<UpsertApplicationRequest, UpsertApplicationResponse>
{
    public async Task<UpsertApplicationResponse> Handle(UpsertApplicationRequest request, CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.GetCandidateByEmail(request.Email);

        var validationResult = new ValidationResult();
        if (candidate == null)
        {
            validationResult.AddError(nameof(candidate), "CandidateAccount does not exist");
            throw new ValidationException(validationResult.DataAnnotationResult,null, null);
        }
        
        var application = await applicationRepository.Upsert(new ApplicationEntity
        {
            VacancyReference = request.VacancyReference,
            CandidateId = candidate.Id,
            Status = (short)request.Status,
            DisabilityConfidenceStatus = (short)request.IsDisabilityConfidenceComplete,
            JobsStatus = (short)request.IsApplicationQuestionsComplete,
            QualificationsStatus = (short)request.IsEducationHistoryComplete,
            WorkExperienceStatus = (short)request.IsInterviewAdjustmentsComplete,
            TrainingCoursesStatus = (short)request.IsWorkHistoryComplete,
            DisabilityStatus = request.DisabilityStatus
        });

        return new UpsertApplicationResponse
        {
            Application = application.Item1,
            IsCreated = application.Item2
        };
    }
}