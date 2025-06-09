using MediatR;
using SFA.DAS.CandidateAccount.Data.EmploymentLocation;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetEmploymentLocations
{
    public class GetEmploymentLocationsQueryHandler(IEmploymentLocationRepository employmentLocationRepository)
        : IRequestHandler<GetEmploymentLocationsQuery, GetEmploymentLocationsQueryResult>
    {
        public async Task<GetEmploymentLocationsQueryResult> Handle(GetEmploymentLocationsQuery request, CancellationToken cancellationToken)
        {
            return await employmentLocationRepository.Get(request.ApplicationId, request.CandidateId, cancellationToken);
        }
    }    
}