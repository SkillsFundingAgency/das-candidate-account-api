using System.Collections.ObjectModel;

namespace SFA.DAS.CandidateAccount.Domain.Application;

public class QualificationReferenceEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public virtual Collection<QualificationEntity> QualificationEntity { get; set; }
}