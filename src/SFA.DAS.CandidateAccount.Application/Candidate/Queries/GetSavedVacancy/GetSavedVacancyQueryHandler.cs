using MediatR;
using SFA.DAS.CandidateAccount.Data.SavedVacancy;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetSavedVacancy
{
    public record GetSavedVacancyQueryHandler(ISavedVacancyRepository Repository) : IRequestHandler<GetSavedVacancyQuery, GetSavedVacancyQueryResult>
    {
        public async Task<GetSavedVacancyQueryResult> Handle(GetSavedVacancyQuery request, CancellationToken cancellationToken)
        {
            var result = await Repository.Get(request.CandidateId, request.VacancyId);

            if (result is null) return new GetSavedVacancyQueryResult();

            return new GetSavedVacancyQueryResult
            {
                Id = result.Id,
                CandidateId = result.CandidateId,
                VacancyReference = result.VacancyReference,
                VacancyId = result.VacancyId,
                CreatedOn = result.CreatedOn
            };
        }
    }
}