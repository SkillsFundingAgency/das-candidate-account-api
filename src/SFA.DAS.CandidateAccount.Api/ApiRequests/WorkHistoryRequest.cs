namespace SFA.DAS.CandidateAccount.Api.ApiRequests
{
    public class WorkHistoryRequest
    {
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
