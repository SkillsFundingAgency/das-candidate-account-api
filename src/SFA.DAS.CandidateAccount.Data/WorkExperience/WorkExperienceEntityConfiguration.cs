using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.WorkExperience;

public class WorkExperienceEntityConfiguration : IEntityTypeConfiguration<WorkExperienceEntity>
{
    public void Configure(EntityTypeBuilder<WorkExperienceEntity> builder)
    {
        builder.ToTable("WorkExperience");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Employer).HasColumnName("Employer").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.JobTitle).HasColumnName("JobTitle").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.FromYear).HasColumnName("FromYear").HasColumnType("smallint").IsRequired();
        builder.Property(x => x.ToYear).HasColumnName("ToYear").HasColumnType("smallint").IsRequired();
        builder.Property(x => x.ApplicationId).HasColumnName("ApplicationId").HasColumnType("uniqueidentifier").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Description).HasColumnName("Description").HasColumnType("varchar").IsRequired();
    }
}

