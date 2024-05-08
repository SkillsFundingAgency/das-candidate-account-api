using MediatR;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;

public class UpsertApplicationCommandHandler(
    IApplicationRepository applicationRepository,
    IAdditionalQuestionRepository additionalQuestionRepository)
    : IRequestHandler<UpsertApplicationCommand, UpsertApplicationCommandResponse>
{
    public async Task<UpsertApplicationCommandResponse> Handle(UpsertApplicationCommand command, CancellationToken cancellationToken)
    {
        if(! await applicationRepository.Exists(command.CandidateId, command.VacancyReference))
        {
            var previousApplications = await applicationRepository.GetByCandidateId(command.CandidateId, null);
            var previousApplication = previousApplications.Where(x => x.Status != (short)ApplicationStatus.Draft).MaxBy(x => x.CreatedDate);

            if (previousApplication != null)
            {
                var requiresDisabilityConfidence = command.IsDisabilityConfidenceComplete == SectionStatus.NotStarted;
                var result = await applicationRepository.Clone(previousApplication.Id, command.VacancyReference, requiresDisabilityConfidence);

                return new UpsertApplicationCommandResponse
                {
                    Application = result,
                    IsCreated = true
                };
            }
        }

        var application = await applicationRepository.Upsert(new ApplicationEntity
        {
            VacancyReference = command.VacancyReference,
            CandidateId = command.CandidateId,
            Status = (short)command.Status,
            DisabilityConfidenceStatus = (short)command.IsDisabilityConfidenceComplete,
            JobsStatus = (short)command.IsApplicationQuestionsComplete,
            QualificationsStatus = (short)command.IsEducationHistoryComplete,
            WorkExperienceStatus = (short)command.IsInterviewAdjustmentsComplete,
            TrainingCoursesStatus = (short)command.IsWorkHistoryComplete,
            AdditionalQuestion1Status = (short)command.IsAdditionalQuestion1Complete,
            AdditionalQuestion2Status = (short) command.IsAdditionalQuestion2Complete,
            DisabilityStatus = command.DisabilityStatus
        });

        foreach (var additionalQuestion in command.AdditionalQuestions)
        {
            if (additionalQuestion is null) break;
            var question =
                await additionalQuestionRepository.Get(application.Item1.Id, command.CandidateId, additionalQuestion, cancellationToken);

            if (question is null)
            {
                await additionalQuestionRepository.UpsertAdditionalQuestion(new AdditionalQuestion
                {
                    Id = Guid.NewGuid(),
                    ApplicationId = application.Item1.Id,
                    CandidateId = command.CandidateId,
                    QuestionText = additionalQuestion,
                    Answer = string.Empty,
                }, command.CandidateId);
            }
        }

        return new UpsertApplicationCommandResponse
        {
            Application = application.Item1,
            IsCreated = application.Item2
        };
    }
}