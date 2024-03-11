using System.Collections.ObjectModel;

namespace SFA.DAS.CandidateAccount.Domain.Application;

public class QualificationReferenceEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Lazy<Collection<QualificationEntity>> QualificationEntity { get; set; }
}