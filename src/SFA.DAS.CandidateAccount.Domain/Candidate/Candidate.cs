using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Domain.Candidate;

public class Candidate
{
    public static implicit operator Candidate?(CandidateEntity? source)
    {
        if (source == null)
        {
            return null;
        }
        return new Candidate
        {
            Id = source.Id,
            Email = source.Email,
            FirstName = source.FirstName,
            LastName = source.LastName,
            GovUkIdentifier = source.GovUkIdentifier,
            MiddleNames = source.MiddleNames,
            PhoneNumber = source.PhoneNumber,
            DateOfBirth = source.DateOfBirth,
            CreatedOn = source.CreatedOn,
            UpdatedOn = source.UpdatedOn,
            TermsOfUseAcceptedOn = source.TermsOfUseAcceptedOn,
            Status = (CandidateStatus) source.Status,
            Address = source.Address
        };
    }

    public Address? Address { get; set; }

    public string GovUkIdentifier { get; set; } = null!;

    public string? LastName { get; set; }

    public string? FirstName { get; set; }
    public string? MiddleNames { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? TermsOfUseAcceptedOn { get; set; }
    public CandidateStatus? Status { get; set; }

    public string? Email { get; set; }

    public Guid Id { get; set; }
}