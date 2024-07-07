using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.ReferenceData.Queries.GetAvailableQualifications;

public class GetAvailableQualificationsQueryResult
{
    public List<QualificationReference> QualificationReferences { get; set; }
}