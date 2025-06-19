using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsByVacancyReference
{
    public class GetApplicationsByVacancyReferenceQueryHandler(IApplicationRepository applicationRepository)
        : IRequestHandler<GetApplicationsByVacancyReferenceQuery, GetApplicationsByVacancyReferenceQueryResult>
    {
        public async Task<GetApplicationsByVacancyReferenceQueryResult> Handle(GetApplicationsByVacancyReferenceQuery request, CancellationToken cancellationToken)
        {
            var applicationEntities = await applicationRepository.GetApplicationsByVacancyReference(request.VacancyReference);

            return new GetApplicationsByVacancyReferenceQueryResult
            {
                Applications = applicationEntities.Select(app => (Domain.Application.ApplicationDetail)app).ToList()
            };
        }
    }
}