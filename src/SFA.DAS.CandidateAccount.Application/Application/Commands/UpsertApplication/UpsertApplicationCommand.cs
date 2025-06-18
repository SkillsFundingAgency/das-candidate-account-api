using MediatR;
using SFA.DAS.CandidateAccount.Domain;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;

public record UpsertApplicationCommand : IRequest<UpsertApplicationCommandResponse>
{
    public required string VacancyReference { get; init; }
    public Guid CandidateId { get; init; }
    public ApplicationStatus Status { get; init; }
    public ApprenticeshipTypes ApprenticeshipType { get; set; }
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