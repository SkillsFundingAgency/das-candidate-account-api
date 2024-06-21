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
            builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime").IsRequired();
        }
    }
}

