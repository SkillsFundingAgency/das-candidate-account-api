using MediatR;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Domain.Application;

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

public class GetQualificationQuery : IRequest<GetQualificationQueryResult>
{
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public Guid ApplicationId { get; set; }
}

public class GetQualificationQueryResult
{
    public Qualification? Qualification { get; set; }
}