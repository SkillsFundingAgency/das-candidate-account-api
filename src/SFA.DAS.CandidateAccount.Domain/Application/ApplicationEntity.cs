using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class ApplicationEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string? DisabilityStatus { get; set; }
        public string VacancyReference { get; set; } = null!;
        public short Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public virtual CandidateEntity CandidateEntity { get; set; } = null!;
        public short IsEducationHistoryComplete { get; set; }
        public short IsWorkHistoryComplete { get; set; }
        public short IsApplicationQuestionsComplete { get; set; }
        public short IsInterviewAdjustmentsComplete { get; set; }
        public short IsDisabilityConfidenceComplete { get; set; }
    }
}
