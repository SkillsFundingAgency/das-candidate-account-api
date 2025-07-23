using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByApplicationVacancy;

public class GetCandidatesByApplicationVacancyQueryResult
{
    public List<ApplicationEntity> Candidates { get; set; }
}