namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class SavedVacancy
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string VacancyReference { get; set; }

        public static implicit operator SavedVacancy(SavedVacancyEntity source)
        {
            return new SavedVacancy
            {
                Id = source.Id,
                CandidateId = source.CandidateId,
                VacancyReference = source.VacancyReference
            };
        }
    }
}
