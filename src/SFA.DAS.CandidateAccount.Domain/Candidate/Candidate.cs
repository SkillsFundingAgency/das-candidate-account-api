namespace SFA.DAS.CandidateAccount.Domain.Candidate;

public class Candidate
{
    public static implicit operator Candidate(CandidateEntity source)
    {
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
            Applications = source.Applications.Select(c=>(Application.Application)c)
        };
    }

    public IEnumerable<Application.Application>? Applications { get; set; }

    public string GovUkIdentifier { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }
    public string MiddleNames { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? TermsOfUseAcceptedOn { get; set; }

    public string Email { get; set; }

    public Guid Id { get; set; }
}