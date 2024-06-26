using MediatR;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetQualifications;

public class GetApplicationQualificationsQueryHandler(IQualificationRepository qualificationRepository) : IRequestHandler<GetApplicationQualificationsQuery, GetApplicationQualificationsQueryResult>
{
    public async Task<GetApplicationQualificationsQueryResult> Handle(GetApplicationQualificationsQuery request, CancellationToken cancellationToken)
    {
        var results =
            await qualificationRepository.GetCandidateApplicationQualifications(request.CandidateId,
                request.ApplicationId);

        return new GetApplicationQualificationsQueryResult
        {
            Qualifications = results.Select(x => (Qualification)x!).ToList()
        };
    }
}