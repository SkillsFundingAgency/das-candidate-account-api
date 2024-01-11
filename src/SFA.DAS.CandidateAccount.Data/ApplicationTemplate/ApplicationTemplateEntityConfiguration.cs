using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Application;

public class ApplicationTemplateEntityConfiguration : IEntityTypeConfiguration<ApplicationTemplateEntity>
{
    public void Configure(EntityTypeBuilder<ApplicationTemplateEntity> builder)
    {
        builder.ToTable("ApplicationTemplate");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.CandidateId).HasColumnName("CandidateId").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.DisabilityStatus).HasColumnName("DisabilityStatus").HasColumnType("varchar").HasMaxLength(150).IsRequired(false);
        builder.Property(x => x.VacancyReference).HasColumnName("VacancyReference").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Status).HasColumnName("Status").HasColumnType("tinyint").IsRequired();
        builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate").HasColumnType("datetime").IsRequired();
        builder.Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").HasColumnType("datetime").IsRequired(false);
    }
}