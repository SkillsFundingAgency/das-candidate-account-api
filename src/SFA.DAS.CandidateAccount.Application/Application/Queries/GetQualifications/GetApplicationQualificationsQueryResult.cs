using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetQualifications;

public class GetApplicationQualificationsQueryResult
{
    public required List<Qualification> Qualifications { get; set; }
}