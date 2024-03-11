namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class QualificationEntity
    {
        public Guid Id { get; set; }
        public Guid QualificationReferenceId { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
        public int ToYear { get; set; }
        public bool IsPredicted { get; set; }
        public Guid ApplicationId { get; set; }
        public virtual ApplicationEntity ApplicationEntity { get; set; }
        public virtual QualificationReferenceEntity QualificationReferenceEntity { get; set; }
    }
}
