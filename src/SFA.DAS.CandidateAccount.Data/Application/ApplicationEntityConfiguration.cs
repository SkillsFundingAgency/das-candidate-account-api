using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Application;

public class ApplicationEntityConfiguration : IEntityTypeConfiguration<ApplicationEntity>
{
    public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
    {
        builder.ToTable("Application");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.CandidateId).HasColumnName("CandidateId").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.DisabilityStatus).HasColumnName("DisabilityStatus").HasColumnType("varchar").HasMaxLength(150).IsRequired(false);
        builder.Property(x => x.VacancyReference).HasColumnName("VacancyReference").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Status).HasColumnName("Status").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate").HasColumnType("datetime").IsRequired().HasDefaultValue();
        builder.Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").HasColumnType("datetime").IsRequired(false);
        builder.Property(x => x.IsEducationHistoryComplete).HasColumnName("IsEducationHistoryComplete").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.IsWorkHistoryComplete).HasColumnName("IsWorkHistoryComplete").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.IsApplicationQuestionsComplete).HasColumnName("IsApplicationQuestionsComplete").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.IsInterviewAdjustmentsComplete).HasColumnName("IsInterviewAdjustmentsComplete").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.IsDisabilityConfidenceComplete).HasColumnName("IsDisabilityConfidenceComplete").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);

        builder.HasIndex(c =>  new { c.VacancyReference, c.CandidateId});
        builder.HasIndex(c =>  new { c.CandidateId});
    }
}