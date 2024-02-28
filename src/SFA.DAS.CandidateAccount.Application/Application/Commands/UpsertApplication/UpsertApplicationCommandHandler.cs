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
            await additionalQuestionRepository.UpsertAdditionalQuestion(new AdditionalQuestion
            {
                Answer = string.Empty,
                ApplicationId = application.Item1.Id,
                CandidateId = command.CandidateId,
                Id = Guid.NewGuid(),
                QuestionId = additionalQuestion
            }, command.CandidateId);
        }

        return new UpsertApplicationCommandResponse
        {
            Application = application.Item1,
            IsCreated = application.Item2
        };
    }
}