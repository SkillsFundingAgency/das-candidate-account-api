namespace SFA.DAS.CandidateAccount.Domain.Application;

public class ApplicationEntity
{
    public Guid Id { get; set; }
    public required string VacancyReference { get; set; }
    public short Status { get; set; }
}