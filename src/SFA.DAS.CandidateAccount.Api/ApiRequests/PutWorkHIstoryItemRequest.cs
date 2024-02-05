using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.ApiRequests
{
    public class PutWorkHIstoryItemRequest
    {
        public string Employer { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public WorkHistoryType WorkHistoryType { get; set; }
    }
}
