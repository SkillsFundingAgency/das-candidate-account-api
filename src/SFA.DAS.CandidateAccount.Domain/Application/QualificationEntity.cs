﻿namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class QualificationEntity
    {
        public static implicit operator QualificationEntity(Qualification source)
        {
            return new QualificationEntity
            {
                Id = source.Id,
                Grade = source.Grade,
                Subject = source.Subject,
                ToYear = source.ToYear.Value,
                IsPredicted = source.IsPredicted ?? false,
                QualificationReferenceId = source.QualificationReference.Id,
                ApplicationId = source.Application.Id
            };
        }
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
