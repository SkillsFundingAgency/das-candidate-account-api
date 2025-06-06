﻿namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetSavedVacancy
{
    public record GetSavedVacancyQueryResult
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string? VacancyReference { get; set; }
        public string? VacancyId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}