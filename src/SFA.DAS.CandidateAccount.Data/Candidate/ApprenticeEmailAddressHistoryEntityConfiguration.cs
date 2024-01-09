using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.Candidate;

public class ApprenticeEmailAddressHistoryEntityConfiguration : IEntityTypeConfiguration<ApprenticeEmailAddressHistoryEntity>
{
    public void Configure(EntityTypeBuilder<ApprenticeEmailAddressHistoryEntity> builder)
    {
        builder.ToTable("ApprenticeEmailAddressHistory");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.EmailAddress).HasColumnName("EmailAddress").HasColumnType("varchar").HasMaxLength(255).IsRequired();
        builder.Property(x => x.ChangedOn).HasColumnName("ChangedOn").HasColumnType("DateTime").IsRequired();
        builder.Property(x => x.CandidateId).HasColumnName("CandidateId").HasColumnType("uniqueidentifier").IsRequired();

    }
}