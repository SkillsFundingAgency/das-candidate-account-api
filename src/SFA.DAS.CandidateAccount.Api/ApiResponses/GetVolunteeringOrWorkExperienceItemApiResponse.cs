using SFA.DAS.CandidateAccount.Application.Application.Queries.GetVolunteeringOrWorkExperienceItem;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public class GetVolunteeringOrWorkExperienceItemApiResponse
{
    public Guid Id { get; set; }
    public string Organisation { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid ApplicationId { get; set; }
    public string Description { get; set; }

    public static implicit operator GetVolunteeringOrWorkExperienceItemApiResponse(GetVolunteeringOrWorkExperienceItemQueryResult source)
    {
        return new GetVolunteeringOrWorkExperienceItemApiResponse
        {
            Id = source.Id,
            Organisation = source.Employer,
            StartDate = source.StartDate,
            EndDate = source.EndDate,
            ApplicationId = source.ApplicationId,
            Description = source.Description
        };
    }
}
