using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.ReferenceData.Queries;

public class GetAvailableQualificationsQueryResult
{
    public List<QualificationReference> QualificationReferences { get; set; }
}