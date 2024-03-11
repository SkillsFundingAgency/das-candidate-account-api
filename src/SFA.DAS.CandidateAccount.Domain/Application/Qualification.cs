namespace SFA.DAS.CandidateAccount.Domain.Application;

public class Qualification
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Grade { get; set; }
    public int ToYear { get; set; }
    public bool IsPredicted { get; set; }
    public Application Application { get; set; }
    public QualificationReference QualificationReference { get; set; }
    public static implicit operator Qualification(QualificationEntity source)
    {
        return new Qualification
        {
            Id = source.Id,
            Subject = source.Subject,
            IsPredicted = source.IsPredicted,
            Grade = source.Grade,
            ToYear = source.ToYear,
            Application = source.ApplicationEntity,
            QualificationReference = source.QualificationReferenceEntity
        };
    }
}