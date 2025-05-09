namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class SavedVacancy
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string? VacancyReference { get; set; }
        public string? VacancyId { get; set; }
        public DateTime CreatedOn { get; set; }

        public static implicit operator SavedVacancy?(SavedVacancyEntity? source)
        {
            if (source == null) return null;

            return new SavedVacancy
            {
                Id = source.Id,
                CandidateId = source.CandidateId,
                VacancyReference = source.VacancyReference,
                VacancyId = source.VacancyId,
                CreatedOn = source.CreatedOn,
            };
        }
    }
}
