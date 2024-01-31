namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public record WorkHistory
    {
        public Guid Id { get; set; }
        public WorkHistoryType WorkHistoryType { get; set; }
        public string? Employer { get; set; }
        public string? JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid ApplicationId { get; set; }
        public string? Description { get; set; }

        public static implicit operator WorkHistory(WorkHistoryEntity source)
        {
            return new WorkHistory
            {
                Id = source.Id,
                WorkHistoryType = (WorkHistoryType)source.WorkHistoryType,
                Employer = source.Employer,
                JobTitle = source.JobTitle,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                ApplicationId = source.ApplicationId,
                Description = source.Description,
            };
        }
    }
}
