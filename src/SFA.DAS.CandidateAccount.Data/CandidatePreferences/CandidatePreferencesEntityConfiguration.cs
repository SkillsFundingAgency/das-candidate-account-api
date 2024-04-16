using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.CandidatePreferences;

public class CandidatePreferencesEntityConfiguration : IEntityTypeConfiguration<CandidatePreferencesEntity>
{
    public void Configure(EntityTypeBuilder<CandidatePreferencesEntity> builder)
    {
        builder.ToTable("CandidatePreferences");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.CandidateId).HasColumnName("CandidateId").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.PreferenceId).HasColumnName("PreferenceId").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Status).HasColumnName("Status").HasColumnType("tinyint");
        builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn").HasColumnType("DateTime").IsRequired();
        builder.Property(x => x.UpdatedOn).HasColumnName("UpdatedOn").HasColumnType("DateTime").IsRequired(false);
        builder.Property(x => x.ContactMethod).HasColumnName("ContactMethod").HasColumnType("varchar").HasMaxLength(50).IsRequired();
    }
}