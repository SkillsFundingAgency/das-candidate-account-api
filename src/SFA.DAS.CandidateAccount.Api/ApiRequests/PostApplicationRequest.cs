using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.ApiRequests;

public class PostApplicationRequest
{
    public required LegacyApplication LegacyApplication { get; set; }
}