namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationByVacancyReference
{
    public record GetApplicationByVacancyReferenceQueryResult
    {
        public Domain.Application.Application? Application { get; set; }
    }
}
