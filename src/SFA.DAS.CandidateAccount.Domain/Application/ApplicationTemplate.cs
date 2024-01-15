namespace SFA.DAS.CandidateAccount.Domain.Application;

public class ApplicationTemplate
{
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public string? DisabilityStatus { get; set; }
    public required string VacancyReference { get; set; }
    public short Status { get; set; }

    public static implicit operator ApplicationTemplate(ApplicationTemplateEntity source)
    {
        return new ApplicationTemplate
        {
            Id = source.Id,
            CandidateId = source.CandidateId,
            DisabilityStatus = source.DisabilityStatus,
            VacancyReference = source.VacancyReference,
            Status = source.Status
        };
    }
}