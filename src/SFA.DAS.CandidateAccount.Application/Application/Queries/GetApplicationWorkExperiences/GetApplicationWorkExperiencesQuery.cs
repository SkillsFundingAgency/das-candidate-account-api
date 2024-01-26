using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationWorkExperiences;

public record GetApplicationWorkExperiencesQuery : IRequest<GetApplicationWorkExperiencesQueryResult>
{
    public Guid ApplicationId { get; set; }
    public Guid CandidateId { get; set; }
}