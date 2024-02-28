using MediatR;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;

public class UpsertApplicationCommand : IRequest<UpsertApplicationCommandResponse>
{
    public required string VacancyReference { get; set; }
    public Guid CandidateId { get; set; }
    public ApplicationStatus Status { get; set; }
    public SectionStatus? IsDisabilityConfidenceComplete { get; set; }
    public SectionStatus? IsApplicationQuestionsComplete { get; set; }
    public SectionStatus? IsEducationHistoryComplete { get; set; }
    public SectionStatus? IsInterviewAdjustmentsComplete { get; set; }
    public SectionStatus? IsWorkHistoryComplete { get; set; }
    public SectionStatus? IsAdditionalQuestion1Complete { get; set; }
    public SectionStatus? IsAdditionalQuestion2Complete { get; set; }
    public string? DisabilityStatus { get; set; }
    public List<string?> AdditionalQuestions { get; set; }
}