using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.AboutYou;

public class AboutYouEntityConfiguration : IEntityTypeConfiguration<AboutYouEntity>
{
    public void Configure(EntityTypeBuilder<AboutYouEntity> builder)
    {
        builder.ToTable("AboutYou");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Sex).HasColumnName("Sex").HasColumnType("tinyint");
        builder.Property(x => x.EthnicGroup).HasColumnName("EthnicGroup").HasColumnType("tinyint");
        builder.Property(x => x.EthnicSubGroup).HasColumnName("EthnicSubGroup").HasColumnType("tinyint");
        builder.Property(x => x.IsGenderIdentifySameSexAtBirth).HasColumnName("IsGenderIdentifySameSexAtBirth").HasColumnType("varchar");
        builder.Property(x => x.OtherEthnicSubGroupAnswer).HasColumnName("OtherEthnicSubGroupAnswer").HasColumnType("varchar");
        builder.Property(x => x.CandidateId).HasColumnName("CandidateId").HasColumnType("uniqueidentifier").IsRequired();
    }
}