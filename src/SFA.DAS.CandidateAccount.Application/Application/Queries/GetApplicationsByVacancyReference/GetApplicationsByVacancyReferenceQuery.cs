using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsByVacancyReference
{
    public sealed record GetApplicationsByVacancyReferenceQuery(string VacancyReference)
        : IRequest<GetApplicationsByVacancyReferenceQueryResult>;
}