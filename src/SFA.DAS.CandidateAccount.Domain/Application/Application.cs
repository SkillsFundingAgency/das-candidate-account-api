namespace SFA.DAS.CandidateAccount.Domain.Application;

public class Application
{
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public string? DisabilityStatus { get; set; }
    public required string VacancyReference { get; set; }
    public short Status { get; set; }

    public static implicit operator Application(ApplicationEntity source)
    {
        return new Application
        {
            Id = source.Id,
            CandidateId = source.CandidateId,
            DisabilityStatus = source.DisabilityStatus,
            VacancyReference = source.VacancyReference,
            Status = source.Status
        };
    }
}