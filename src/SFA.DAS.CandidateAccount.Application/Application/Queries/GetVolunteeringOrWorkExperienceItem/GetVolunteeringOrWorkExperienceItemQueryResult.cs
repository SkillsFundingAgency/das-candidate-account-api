using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetVolunteeringOrWorkExperienceItem;
public class GetVolunteeringOrWorkExperienceItemQueryResult
{
    public Guid Id { get; set; }
    public string Employer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid ApplicationId { get; set; }
    public string Description { get; set; }

    public static implicit operator GetVolunteeringOrWorkExperienceItemQueryResult(WorkHistoryEntity source)
    {
        return new GetVolunteeringOrWorkExperienceItemQueryResult
        {
            Id = source.Id,
            ApplicationId = source.ApplicationId,
            Description = source.Description,
            Employer = source.Employer,
            EndDate = source.EndDate,
            StartDate = source.StartDate
        };
    }
}
