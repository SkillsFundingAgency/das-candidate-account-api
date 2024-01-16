using System.ComponentModel.DataAnnotations;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.ApiRequests;

public class ApplicationRequest
{
    [Required]
    public required string Email { get; set; }
    
    public ApplicationStatus Status { get; set; }
    public string? DisabilityStatus { get; set; }
    public SectionStatus IsApplicationQuestionsComplete { get; set; }
    public SectionStatus IsDisabilityConfidenceComplete { get; set; }
    public SectionStatus IsEducationHistoryComplete { get; set; }
    public SectionStatus IsInterviewAdjustmentsComplete { get; set; }
    public SectionStatus IsWorkHistoryComplete { get; set; }
}