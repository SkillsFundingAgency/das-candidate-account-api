using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationQualificationsByType;

public class GetApplicationQualificationsByTypeQueryResult
{
    public required List<Qualification> Qualifications { get; set; }
}