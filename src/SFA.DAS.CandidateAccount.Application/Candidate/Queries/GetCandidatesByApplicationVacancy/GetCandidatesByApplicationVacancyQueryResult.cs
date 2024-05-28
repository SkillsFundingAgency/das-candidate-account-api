using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByApplicationVacancy;

public class GetCandidatesByApplicationVacancyQueryResult
{
    public List<CandidateEntity> Candidates { get; set; }
}