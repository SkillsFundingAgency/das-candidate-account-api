using System.ComponentModel.DataAnnotations;
using SFA.DAS.CandidateAccount.Domain;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.ApiRequests;

public class ApplicationRequest
{
    [Required]
    public required Guid CandidateId { get; set; }
    public ApplicationStatus Status { get; set; }
    public ApprenticeshipTypes ApprenticeshipType { get; set; }
    public string? DisabilityStatus { get; set; }
    public SectionStatus IsApplicationQuestionsComplete { get; set; }
    public SectionStatus IsDisabilityConfidenceComplete { get; set; }
    public SectionStatus IsEducationHistoryComplete { get; set; }
    public SectionStatus IsInterviewAdjustmentsComplete { get; set; }
    public SectionStatus IsWorkHistoryComplete { get; set; }
    public SectionStatus IsAdditionalQuestion1Complete { get; set; }
    public SectionStatus IsAdditionalQuestion2Complete { get; set; }
    public SectionStatus IsEmploymentLocationComplete { get; set; }
    public List<KeyValuePair<int, string>>? AdditionalQuestions { get; set; } = [];
    public Location? EmploymentLocation { get; set; }
}