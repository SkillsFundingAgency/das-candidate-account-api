namespace SFA.DAS.CandidateAccount.Api.ApiRequests;

public class PostCandidateRequest : CandidateRequest
{
    
    public string GovUkIdentifier { get; set; }
    
}

public class PutCandidateRequest : CandidateRequest
{
    
}

public abstract class CandidateRequest
{
    public DateTime DateOfBirth { get; set; }
    public DateTime? TermsOfUseAcceptedOn { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MiddleNames { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}