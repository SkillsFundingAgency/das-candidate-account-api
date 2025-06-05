using MediatR;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Common.Domain.Models;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;

public record UpsertApplicationCommand : IRequest<UpsertApplicationCommandResponse>
{
    public required VacancyReference VacancyReference { get; init; }
    public Guid CandidateId { get; init; }
    public ApplicationStatus Status { get; init; }
    public SectionStatus? IsDisabilityConfidenceComplete { get; init; }
    public SectionStatus? IsApplicationQuestionsComplete { get; init; }
    public SectionStatus? IsEducationHistoryComplete { get; init; }
    public SectionStatus? IsInterviewAdjustmentsComplete { get; init; }
    public SectionStatus? IsWorkHistoryComplete { get; init; }
    public SectionStatus? IsAdditionalQuestion1Complete { get; init; }
    public SectionStatus? IsAdditionalQuestion2Complete { get; init; }
    public string? DisabilityStatus { get; init; }
    public List<KeyValuePair<int, string>>? AdditionalQuestions { get; init; } = [];
}