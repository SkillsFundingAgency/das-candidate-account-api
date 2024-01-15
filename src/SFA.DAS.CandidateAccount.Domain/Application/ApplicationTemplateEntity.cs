using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class ApplicationTemplateEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string? DisabilityStatus { get; set; }
        public string VacancyReference { get; set; } = null!;
        public short Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public virtual CandidateEntity CandidateEntity { get; set; } = null!;
    }
}
