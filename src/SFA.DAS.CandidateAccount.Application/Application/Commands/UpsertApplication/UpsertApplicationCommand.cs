using MediatR;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;

public class UpsertApplicationCommand : IRequest<UpsertApplicationCommandResponse>
{
    public string VacancyReference { get; set; }
    public string Email { get; set; }
    public ApplicationStatus Status { get; set; }
    public SectionStatus? IsDisabilityConfidenceComplete { get; set; }
    public SectionStatus? IsApplicationQuestionsComplete { get; set; }
    public SectionStatus? IsEducationHistoryComplete { get; set; }
    public SectionStatus? IsInterviewAdjustmentsComplete { get; set; }
    public SectionStatus? IsWorkHistoryComplete { get; set; }
    public string? DisabilityStatus { get; set; }
}