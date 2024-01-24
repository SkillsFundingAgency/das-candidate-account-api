using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Domain.Candidate;

public record CandidateEntity
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleNames { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? TermsOfUseAcceptedOn { get; set; }
    public required string GovUkIdentifier { get; set; }
    
    public virtual ICollection<ApplicationEntity>? Applications { get; set; } = null!;
}