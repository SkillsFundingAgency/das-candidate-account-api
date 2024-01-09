using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Candidate;

public class EducationEntityConfiguration : IEntityTypeConfiguration<EducationEntity>
{
    public void Configure(EntityTypeBuilder<EducationEntity> builder)
    {
        builder.ToTable("Education");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Institution).HasColumnName("Institution").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.FromYear).HasColumnName("FromYear").HasColumnType("smallint").IsRequired();
        builder.Property(x => x.ToYear).HasColumnName("ToYear").HasColumnType("smallint").IsRequired();
        builder.Property(x => x.ApplicationTemplateId).HasColumnName("ApplicationTemplateId").HasColumnType("uniqueidentifier").IsRequired();
    }
}