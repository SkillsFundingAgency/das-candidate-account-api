namespace SFA.DAS.CandidateAccount.Api.ApiRequests;

public class CandidateRequest
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string GovUkIdentifier { get; set; }
    public DateTime DateOfBirth { get; set; }
}