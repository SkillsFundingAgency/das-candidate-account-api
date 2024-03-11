using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Qualification;

public class QualificationEntityConfiguration : IEntityTypeConfiguration<QualificationEntity>
{
    public void Configure(EntityTypeBuilder<QualificationEntity> builder)
    {
        builder.ToTable("Qualification");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.QualificationReferenceId).HasColumnName("QualificationReferenceId").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Subject).HasColumnName("Subject").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Grade).HasColumnName("Grade").HasColumnType("varchar").HasMaxLength(150).IsRequired(false);
        builder.Property(x => x.ToYear).HasColumnName("ToYear").HasColumnType("smallint").IsRequired(false);
        builder.Property(x => x.IsPredicted).HasColumnName("IsPredicted").HasColumnType("bit").IsRequired();
        builder.Property(x => x.ApplicationId).HasColumnName("ApplicationId").HasColumnType("uniqueidentifier").IsRequired();
        
        builder
            .HasOne(c => c.ApplicationEntity)
            .WithMany(c => c.QualificationEntities)
            .HasForeignKey(c => c.ApplicationId)
            .HasPrincipalKey(c => c.Id);

        builder.HasOne(c => c.QualificationReferenceEntity)
            .WithMany(c => c.QualificationEntity.Value)
            .HasForeignKey(c => c.QualificationReferenceId)
            .HasPrincipalKey(c => c.Id);
    }
}