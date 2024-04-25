using MediatR;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplications
{
    public class GetApplicationsQuery : IRequest<GetApplicationsQueryResult>
    {
        public Guid CandidateId { get; set; }
        public ApplicationStatus? Status { get; set; }
    }
}
