using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetQualifications;

public class GetApplicationQualificationsQuery : IRequest<GetApplicationQualificationsQueryResult>
{
    public Guid ApplicationId { get; set; }
    public Guid CandidateId { get; set; }
}