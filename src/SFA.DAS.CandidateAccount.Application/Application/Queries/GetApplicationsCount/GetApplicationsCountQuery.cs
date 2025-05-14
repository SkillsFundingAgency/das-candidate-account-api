using MediatR;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsCount
{
    public record GetApplicationsCountQuery : IRequest<GetApplicationsCountQueryResult>
    {
        public Guid CandidateId { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}