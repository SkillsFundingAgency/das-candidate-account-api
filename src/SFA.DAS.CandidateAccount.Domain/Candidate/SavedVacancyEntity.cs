namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class SavedVacancyEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string? VacancyReference { get; set; }
        public string? VacancyId { get; set; }
        public DateTime CreatedOn { get; set; }

        public static implicit operator SavedVacancyEntity?(SavedVacancy? source)
        {
            if (source == null) return null;

            return new SavedVacancyEntity
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
