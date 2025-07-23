
namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsByVacancyReference
{
    public record GetApplicationsByVacancyReferenceQueryResult
    {
        public List<Domain.Application.ApplicationDetail> Applications { get; init; } = [];
    }
}
