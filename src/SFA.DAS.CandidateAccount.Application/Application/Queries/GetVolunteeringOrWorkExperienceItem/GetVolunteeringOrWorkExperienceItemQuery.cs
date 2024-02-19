using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetVolunteeringOrWorkExperienceItem;
public class GetVolunteeringOrWorkExperienceItemQuery : IRequest<GetVolunteeringOrWorkExperienceItemQueryResult>
{
    public Guid Id { get; set; }
    public Guid ApplicationId { get; set; }
    public Guid CandidateId { get; set; }
}
