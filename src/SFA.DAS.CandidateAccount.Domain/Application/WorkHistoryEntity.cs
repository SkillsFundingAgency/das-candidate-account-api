namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class WorkHistoryEntity
    {
        public Guid Id { get; set; }
        public byte WorkHistoryType { get; set; }
        public string Employer { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid ApplicationId { get; set; }
        public string Description { get; set; }

        public virtual ApplicationEntity ApplicationEntity { get; set; }

        public static implicit operator WorkHistoryEntity(WorkHistory source)
        {
            return new WorkHistoryEntity
            {
                Id = source.Id,
                WorkHistoryType = (byte)source.WorkHistoryType,
                Employer = source.Employer,
                JobTitle = source.JobTitle,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                ApplicationId = source.ApplicationId,
                Description = source.Description
            };
        }
    }
}
