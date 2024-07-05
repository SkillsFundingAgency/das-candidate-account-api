using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.ApiRequests;

public class PostCandidateRequest : CandidateRequest
{
}

public class PutCandidateRequest : CandidateRequest
{
    
}

public abstract class CandidateRequest
{
    public DateTime? DateOfBirth { get; set; }
    public DateTime? TermsOfUseAcceptedOn { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MiddleNames { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public CandidateStatus? Status { get; set; }
    public string? MigratedEmail { get; set; }
    public Guid? MigratedCandidateId { get; set; }
}