
namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsByVacancyReference;

public record GetAllApplicationsByVacancyReferenceQueryResult
{
    public List<Domain.Application.ApplicationDetail> Applications { get; init; } = [];
}