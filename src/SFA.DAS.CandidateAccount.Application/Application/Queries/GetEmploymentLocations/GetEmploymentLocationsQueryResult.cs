using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetEmploymentLocations
{
    public record GetEmploymentLocationsQueryResult
    {
        public List<EmploymentLocation> EmploymentLocations { get; init; } = [];

        public static implicit operator GetEmploymentLocationsQueryResult(List<EmploymentLocation> source)
        {
            return new GetEmploymentLocationsQueryResult
            {
                EmploymentLocations = source.Select(x => new EmploymentLocation
                {
                    Addresses = x.Addresses,
                    EmployerLocationOption = x.EmployerLocationOption,
                    EmploymentLocationInformation = x.EmploymentLocationInformation,
                    ApplicationId = x.ApplicationId,
                    Id = x.Id,
                }).ToList()
            };
        }
    }
}
