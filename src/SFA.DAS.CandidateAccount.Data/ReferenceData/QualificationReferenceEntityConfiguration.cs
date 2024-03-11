using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.ReferenceData;

public class QualificationReferenceEntityConfiguration : IEntityTypeConfiguration<QualificationReferenceEntity>
{
    public void Configure(EntityTypeBuilder<QualificationReferenceEntity> builder)
    {
        builder.ToTable("QualificationReference");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(500).IsRequired();
        builder.Property(x => x.Order).HasColumnName("Order").HasColumnType("tinyint").IsRequired();

    }
}