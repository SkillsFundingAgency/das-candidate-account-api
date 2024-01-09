using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Application;

public class ApplicationTemplateEntityConfiguration : IEntityTypeConfiguration<ApplicationTemplateEntity>
{
    public void Configure(EntityTypeBuilder<ApplicationTemplateEntity> builder)
    {
        builder.ToTable("ApprenticeTemplate");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.CandidateId).HasColumnName("CandidateId").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.DisabilityStatus).HasColumnName("DisabilityStatus").HasColumnType("varchar").HasMaxLength(150).IsRequired();
    }
}