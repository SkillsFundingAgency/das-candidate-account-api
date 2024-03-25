using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public class GetQualificationsApiResponse
{
    public List<Qualification> Qualifications { get; set; } = [];
}