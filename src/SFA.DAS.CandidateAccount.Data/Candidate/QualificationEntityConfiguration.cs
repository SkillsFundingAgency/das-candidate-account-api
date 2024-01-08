using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.Candidate
{
    public class QualificationEntityConfiguration : IEntityTypeConfiguration<QualificationEntity>
    {
        public void Configure(EntityTypeBuilder<QualificationEntity> builder)
        {
            builder.ToTable("Qualification");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Type).HasColumnName("Type").HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.Subject).HasColumnName("Subject").HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.Grade).HasColumnName("Grade").HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.ToYear).HasColumnName("ToYear").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.IsPredicted).HasColumnName("IsPredicted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ApplicationTemplateId).HasColumnName("ApplicationTemplateId").HasColumnType("varchar").IsRequired();
        }
    }
}
