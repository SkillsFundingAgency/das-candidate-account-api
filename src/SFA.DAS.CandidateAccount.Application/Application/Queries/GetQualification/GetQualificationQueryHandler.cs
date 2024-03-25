using MediatR;
using SFA.DAS.CandidateAccount.Data.Qualification;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetQualification;

public class GetQualificationQueryHandler(IQualificationRepository qualificationRepository) : IRequestHandler<GetQualificationQuery, GetQualificationQueryResult>
{
    public async Task<GetQualificationQueryResult> Handle(GetQualificationQuery request, CancellationToken cancellationToken)
    {
        var result =
            await qualificationRepository.GetCandidateApplicationQualificationById(request.CandidateId,
                request.ApplicationId, request.Id);

        return new GetQualificationQueryResult
        {
            Qualification = result
        };
    }
}