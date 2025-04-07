using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetEmploymentLocations
{
    public record GetEmploymentLocationsQuery : IRequest<GetEmploymentLocationsQueryResult>
    {
        public Guid ApplicationId { get; init; }
        public Guid CandidateId { get; set; }
    }
}