namespace SFA.DAS.CandidateAccount.Api.ApiRequests;

public class QualificationRequest
{
    public Guid Id { get; set; }
    public int? ToYear { get; set; }
    public string? Grade { get; set; }
    public string? Subject { get; set; }
    public bool? IsPredicted { get; set; }
    public string? AdditionalInformation { get; set; }
    public Guid QualificationReferenceId { get; set; }
}