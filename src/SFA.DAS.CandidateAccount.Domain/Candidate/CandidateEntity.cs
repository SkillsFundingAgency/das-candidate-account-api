using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Domain.Candidate;

public record CandidateEntity
{
    public static implicit operator CandidateEntity(Candidate source)
    {
        return new CandidateEntity
        {
            Id = source.Id,
            Email = source.Email,
            FirstName = source.FirstName,
            LastName = source.LastName,
            GovUkIdentifier = source.GovUkIdentifier,
            DateOfBirth = source.DateOfBirth,
            CreatedOn = source.CreatedOn,
            TermsOfUseAcceptedOn = source.TermsOfUseAcceptedOn,
            MiddleNames = source.MiddleNames,
            PhoneNumber = source.PhoneNumber,
            UpdatedOn = source.UpdatedOn,
            MigratedEmail = source.MigratedEmail,
            Status = (short)source.Status
        };
    }
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleNames { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? TermsOfUseAcceptedOn { get; set; }
    public required string GovUkIdentifier { get; set; }
    public short Status { get; set; }
    public string? MigratedEmail { get; set; }

    public virtual AddressEntity? Address { get; set; }
    
    public virtual ICollection<CandidatePreferencesEntity> CandidatePreferences { get; set; }
    
    public virtual AboutYouEntity? AboutYou { get; set; } = null;

    public virtual ICollection<ApplicationEntity>? Applications { get; set; } = null!;
}