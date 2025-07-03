using SFA.DAS.Common.Domain.Models;

namespace SFA.DAS.CandidateAccount.Api.ApiRequests
{
    public class SavedVacancyRequest
    {
        public VacancyReference? VacancyReference { get; set; }
        public string? VacancyId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
