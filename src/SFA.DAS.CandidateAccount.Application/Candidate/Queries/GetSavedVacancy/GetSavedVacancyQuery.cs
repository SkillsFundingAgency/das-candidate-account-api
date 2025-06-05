using MediatR;
using SFA.DAS.Common.Domain.Models;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetSavedVacancy
{
    public record GetSavedVacancyQuery(Guid CandidateId, string? VacancyId, VacancyReference? VacancyReference)
        : IRequest<GetSavedVacancyQueryResult>;
}