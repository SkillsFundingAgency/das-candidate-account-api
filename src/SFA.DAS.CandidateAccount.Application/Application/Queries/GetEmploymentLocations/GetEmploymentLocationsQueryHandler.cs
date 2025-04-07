using MediatR;
using SFA.DAS.CandidateAccount.Data.EmploymentLocation;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetEmploymentLocations
{
    public class GetEmploymentLocationsQueryHandler(IEmploymentLocationRepository employmentLocationRepository)
        : IRequestHandler<GetEmploymentLocationsQuery, GetEmploymentLocationsQueryResult>
    {
        public async Task<GetEmploymentLocationsQueryResult> Handle(GetEmploymentLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await employmentLocationRepository.GetAll(request.ApplicationId, request.CandidateId, cancellationToken);
            return new GetEmploymentLocationsQueryResult
            {
                EmploymentLocations = locations.Select(l => (EmploymentLocation)l).ToList()
            };
        }
    }    
}