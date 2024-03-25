namespace SFA.DAS.CandidateAccount.Domain.Application;

public class QualificationReference
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    
    public static implicit operator QualificationReference(QualificationReferenceEntity source)
    {
        return new QualificationReference
        {
            Id = source.Id,
            Name = source.Name
        };
    }
}