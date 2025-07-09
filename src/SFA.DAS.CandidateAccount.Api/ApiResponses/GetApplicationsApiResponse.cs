using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses
{
    public record GetApplicationsApiResponse
    {
        public List<Application> Applications { get; init; } = [];

        public static implicit operator GetApplicationsApiResponse(ApplicationDetail[] applications)
        {
            return new GetApplicationsApiResponse
            {
                Applications = applications.Select(app => (Application)app).ToList()
            };
        }

        public record Application
        {
            public Guid Id { get; set; }
            public Guid CandidateId { get; set; }
            public required string VacancyReference { get; set; }
            public DateTime? SubmittedDate { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime? WithdrawnDate { get; set; }
            public EmploymentLocation? EmploymentLocation { get; set; }
            public Candidate? Candidate { get; set; }
            public ApplicationStatus Status { get; set; }

            public static implicit operator Application(ApplicationDetail application)
            {
                return new Application
                {
                    Id = application.Id,
                    CandidateId = application.CandidateId,
                    VacancyReference = application.VacancyReference,
                    SubmittedDate = application.SubmittedDate,
                    CreatedDate = application.CreatedDate,
                    WithdrawnDate = application.WithdrawnDate,
                    EmploymentLocation = application.EmploymentLocation,
                    Candidate = application.Candidate,
                    Status = application.Status
                };
            }
        }

        public record Candidate
        {
            public Guid Id { get; set; }
            public string? Email { get; set; }
            public string? LastName { get; set; }
            public string? FirstName { get; set; }
            public string? MiddleNames { get; set; }

            public static implicit operator Candidate(Domain.Candidate.Candidate candidate)
            {
                return new Candidate
                {
                    Id = candidate.Id,
                    Email = candidate.Email,
                    LastName = candidate.LastName,
                    FirstName = candidate.FirstName,
                    MiddleNames = candidate.MiddleNames,
                };
            }
        }
    }
}
