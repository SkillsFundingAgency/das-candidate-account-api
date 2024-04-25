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
        builder.Property(x => x.SubmittedDate).HasColumnName("SubmittedDate").HasColumnType("datetime").IsRequired(false);
        builder.Property(x => x.ResponseDate).HasColumnName("ResponseDate").HasColumnType("datetime").IsRequired(false);
        builder.Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").HasColumnType("datetime").IsRequired(false);
        builder.Property(x => x.ResponseNotes).HasColumnName("ResponseNotes").HasColumnType("varchar").IsRequired(false);
        builder.Property(x => x.QualificationsStatus).HasColumnName("QualificationsStatus").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.TrainingCoursesStatus).HasColumnName("TrainingCoursesStatus").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.JobsStatus).HasColumnName("JobsStatus").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.WorkExperienceStatus).HasColumnName("WorkExperienceStatus").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.SkillsAndStrengthStatus).HasColumnName("SkillsAndStrengthStatus").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InterestsStatus).HasColumnName("InterestsStatus").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.AdditionalQuestion1Status).HasColumnName("AdditionalQuestion1Status").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.AdditionalQuestion2Status).HasColumnName("AdditionalQuestion2Status").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InterviewAdjustmentsStatus).HasColumnName("InterviewAdjustmentsStatus").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
        builder.Property(x => x.DisabilityConfidenceStatus).HasColumnName("DisabilityConfidenceStatus").HasColumnType("tinyint").IsRequired().HasDefaultValue(0);

        builder.HasIndex(c =>  new { c.VacancyReference, c.CandidateId});
        builder.HasIndex(c =>  new { c.CandidateId});

        builder
            .HasOne(c => c.CandidateEntity)
            .WithMany(c => c.Applications)
            .HasForeignKey(c => c.CandidateId)
            .HasPrincipalKey(c => c.Id);
        
    }
}