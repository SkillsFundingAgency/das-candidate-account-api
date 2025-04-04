namespace SFA.DAS.CandidateAccount.Api.ApiRequests
{
    public class SavedVacancyRequest
    {
        public string VacancyReference { get; set; }
        public required string VacancyId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
