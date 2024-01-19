using System.ComponentModel.DataAnnotations;
using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Application;
using ValidationResult = SFA.DAS.CandidateAccount.Domain.RequestHandlers.ValidationResult;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;

public class UpsertApplicationCommandHandler(
    ICandidateRepository candidateRepository,
    IApplicationRepository applicationRepository)
    : IRequestHandler<UpsertApplicationCommand, UpsertApplicationCommandResponse>
{
    public async Task<UpsertApplicationCommandResponse> Handle(UpsertApplicationCommand command, CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.GetCandidateByEmail(command.Email);

        if (candidate == null)
        {
            var validationResult = new ValidationResult();
            validationResult.AddError(nameof(candidate), "CandidateAccount does not exist");
            throw new ValidationException(validationResult.DataAnnotationResult,null, null);
        }
        
        var application = await applicationRepository.Upsert(new ApplicationEntity
        {
            VacancyReference = command.VacancyReference,
            CandidateId = candidate.Id,
            Status = (short)command.Status,
            DisabilityConfidenceStatus = (short)command.IsDisabilityConfidenceComplete,
            JobsStatus = (short)command.IsApplicationQuestionsComplete,
            QualificationsStatus = (short)command.IsEducationHistoryComplete,
            WorkExperienceStatus = (short)command.IsInterviewAdjustmentsComplete,
            TrainingCoursesStatus = (short)command.IsWorkHistoryComplete,
            DisabilityStatus = command.DisabilityStatus
        });

        return new UpsertApplicationCommandResponse
        {
            Application = application.Item1,
            IsCreated = application.Item2
        };
    }
}