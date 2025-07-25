using MediatR;
using SFA.DAS.Common.Domain.Models;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByApplicationVacancy;

public class GetCandidatesByApplicationVacancyQuery : IRequest<GetCandidatesByApplicationVacancyQueryResult>
{
    public required VacancyReference VacancyReference { get; set; }
    public short? StatusId { get; set; }
    public Guid? PreferenceId { get; set; }
    public bool CanEmailOnly { get; set; }
}