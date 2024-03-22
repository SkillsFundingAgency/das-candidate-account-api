using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.Address;

public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.ToTable("Address");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.AddressLine1).HasColumnName("AddressLine1").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.AddressLine2).HasColumnName("AddressLine2").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.AddressLine3).HasColumnName("AddressLine3").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.AddressLine4).HasColumnName("AddressLine4").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Postcode).HasColumnName("Postcode").HasColumnType("varchar").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Uprn).HasColumnName("Uprn").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.CandidateId).HasColumnName("CandidateId").HasColumnType("uniqueidentifier").IsRequired();
    }
}