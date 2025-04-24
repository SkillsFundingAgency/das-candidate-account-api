using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetEmploymentLocations
{
    public record GetEmploymentLocationsQueryResult
    {
        public EmploymentLocation? EmploymentLocation { get; init; }

        public static implicit operator GetEmploymentLocationsQueryResult(EmploymentLocationEntity? employmentLocation)
        {
            if (employmentLocation == null)
            {
                return new GetEmploymentLocationsQueryResult
                {
                    EmploymentLocation = null
                };
            }
            return new GetEmploymentLocationsQueryResult
            {
                EmploymentLocation = employmentLocation
            };
        }
    }
}
