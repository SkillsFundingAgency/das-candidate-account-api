using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsByVacancyReference;

public class GetAllApplicationsByVacancyReferenceQueryHandler(IApplicationRepository applicationRepository)
    : IRequestHandler<GetAllApplicationsByVacancyReferenceQuery, GetAllApplicationsByVacancyReferenceQueryResult>
{
    public async Task<GetAllApplicationsByVacancyReferenceQueryResult> Handle(GetAllApplicationsByVacancyReferenceQuery request, CancellationToken cancellationToken)
    {
        var applicationEntities = await applicationRepository.GetAllApplicationsByVacancyReference(request.VacancyReference);

        return new GetAllApplicationsByVacancyReferenceQueryResult
        {
            Applications = applicationEntities.Select(app => (Domain.Application.ApplicationDetail)app).ToList()
        };
    }
}