namespace SFA.DAS.CandidateAccount.Domain.Application;

public class Qualification
{
    public Guid Id { get; set; }
    public string? Subject { get; set; }
    public string? Grade { get; set; }
    public string? AdditionalInformation { get; set; }
    public int? ToYear { get; set; }
    public bool? IsPredicted { get; set; }
    public short? QualificationOrder { get; set; }
    public QualificationReference QualificationReference { get; set; }
    public DateTime? CreatedDate { get; set; }

    public static implicit operator Qualification?(QualificationEntity? source)
    {
        if (source == null)
        {
            return null;
        }
        return new Qualification
        {
            Id = source.Id,
            Subject = source.Subject,
            IsPredicted = source.IsPredicted,
            Grade = source.Grade,
            ToYear = source.ToYear,
            QualificationOrder = source.QualificationOrder,
            AdditionalInformation = source.AdditionalInformation,
            QualificationReference = source.QualificationReferenceEntity,
            CreatedDate = source.CreatedDate
        };
    }
}