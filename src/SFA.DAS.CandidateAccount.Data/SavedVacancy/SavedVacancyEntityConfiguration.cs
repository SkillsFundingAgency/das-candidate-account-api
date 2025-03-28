using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.SavedVacancy
{
    public class SavedVacancyEntityConfiguration : IEntityTypeConfiguration<SavedVacancyEntity>
    {
        public void Configure(EntityTypeBuilder<SavedVacancyEntity> builder)
        {
            builder.ToTable("SavedVacancy");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.VacancyReference).HasColumnName("VacancyReference").HasColumnType("varchar").IsRequired();
            builder.Property(x => x.CandidateId).HasColumnName("CandidateId").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.VacancyId).HasColumnName("VacancyId").HasColumnType("nvarchar(150)").IsRequired();
            builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime").IsRequired();

            // Define indexes
            builder.HasIndex(x => new { x.CandidateId, x.VacancyReference })
                .HasDatabaseName("IX_SavedVacancy_CandidateIdVacancyReference")
                .IsUnique(false); // Ensure it's not enforcing uniqueness unless required

            builder.HasIndex(x => x.CandidateId)
                .HasDatabaseName("IX_SavedVacancy_CandidateId");

            builder.HasIndex(x => new { x.CandidateId, x.VacancyId })
                .HasDatabaseName("IX_SavedVacancy_CandidateIdVacancyId");
        }
    }
}

