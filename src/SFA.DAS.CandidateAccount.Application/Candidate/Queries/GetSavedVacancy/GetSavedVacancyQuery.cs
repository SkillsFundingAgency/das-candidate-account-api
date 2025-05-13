using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetSavedVacancy
{
    public record GetSavedVacancyQuery(Guid CandidateId, string? VacancyId, string? VacancyReference)
        : IRequest<GetSavedVacancyQueryResult>;
}