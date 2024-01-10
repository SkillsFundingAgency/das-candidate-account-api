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
        builder.Property(x => x.VacancyReference).HasColumnName("VacancyReference").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Status).HasColumnName("Status").HasColumnType("tinyint").IsRequired();
    }
}