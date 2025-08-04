using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsByVacancyReference;

public sealed record GetAllApplicationsByVacancyReferenceQuery(string VacancyReference)
    : IRequest<GetAllApplicationsByVacancyReferenceQueryResult>;