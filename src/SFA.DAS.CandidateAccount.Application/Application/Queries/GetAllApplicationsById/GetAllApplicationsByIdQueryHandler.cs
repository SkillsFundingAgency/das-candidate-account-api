using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetAllApplicationsById
{
    public class GetAllApplicationsByIdQueryHandler(
        IApplicationRepository repository)
        : IRequestHandler<GetAllApplicationsByIdQuery, GetAllApplicationsByIdQueryResult>
    {
        public async Task<GetAllApplicationsByIdQueryResult> Handle(GetAllApplicationsByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllById(request.ApplicationIds,
                request.IncludeDetails,
            cancellationToken);
            
            return new GetAllApplicationsByIdQueryResult(result
                .Select(x => request.IncludeDetails 
                    ? (ApplicationDetail)x 
                    : (Domain.Application.Application)x)
                .ToList());
        }
    }
}